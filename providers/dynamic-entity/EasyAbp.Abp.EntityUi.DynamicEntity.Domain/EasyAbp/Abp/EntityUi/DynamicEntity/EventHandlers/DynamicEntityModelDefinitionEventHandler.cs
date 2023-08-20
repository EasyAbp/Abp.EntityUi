using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.FieldDefinitions;
using EasyAbp.Abp.DynamicEntity.Localization;
using EasyAbp.Abp.DynamicEntity.ModelDefinitions;
using EasyAbp.Abp.DynamicMenu.MenuItems;
using EasyAbp.Abp.EntityUi.Entities;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.EntityUi.DynamicEntity.EventHandlers
{
    [UnitOfWork]
    public class DynamicEntityModelDefinitionEventHandler :
        IDistributedEventHandler<EntityCreatedEto<ModelDefinitionEto>>,
        IDistributedEventHandler<EntityUpdatedEto<ModelDefinitionEto>>,
        IDistributedEventHandler<EntityDeletedEto<ModelDefinitionEto>>,
        ITransientDependency
    {
        private const string ModuleName = "EasyAbp.Abp.DynamicEntity";
        private const string Namespace = "EasyAbp.Abp.DynamicEntity.DynamicEntities";
        
        private readonly IEntityRepository _entityRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMenuItemNameCalculator _menuItemNameCalculator;

        public DynamicEntityModelDefinitionEventHandler(
            IEntityRepository entityRepository,
            IMenuItemRepository menuItemRepository,
            IMenuItemNameCalculator menuItemNameCalculator)
        {
            _entityRepository = entityRepository;
            _menuItemRepository = menuItemRepository;
            _menuItemNameCalculator = menuItemNameCalculator;
        }
        
        public virtual async Task HandleEventAsync(EntityCreatedEto<ModelDefinitionEto> eventData)
        {
            var entityName = eventData.Entity.Name;

            var entity = await _entityRepository.FindAsync(x => x.Name == entityName);

            if (entity == null)
            {
                entity = new Entity(await CreateEntityDataModelAsync(eventData.Entity, null));

                entity.SetProperty(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName,
                    eventData.Entity.Id);

                await _entityRepository.InsertAsync(entity, true);
            }

            var menuItem = await _menuItemRepository.FindAsync(x =>
                x.Name == _menuItemNameCalculator.GetName(ModuleName, eventData.Entity.Name));

            if (menuItem == null)
            {
                var moduleMenuItem = await GetOrCreateModuleMenuItemAsync();

                menuItem = await GenerateEntityMenuItemAsync(eventData.Entity);
                    
                moduleMenuItem.MenuItems.Add(menuItem);
                
                await _menuItemRepository.UpdateAsync(moduleMenuItem, true);
            }
        }

        protected virtual Task<MenuItem> GenerateModuleMenuItemAsync()
        {
            return Task.FromResult(new MenuItem(
                parentName: null,
                inAdministration: true,
                name: _menuItemNameCalculator.GetName(ModuleName),
                displayName: _menuItemNameCalculator.GetDisplayName(ModuleName),
                url: null,
                urlMvc: null,
                urlBlazor: null,
                urlAngular: null,
                permission: null,
                order: null,
                icon: "fa fa-cubes",
                target: null,
                isDisabled: false,
                lResourceTypeName: typeof(DynamicEntityResource).FullName,
                lResourceTypeAssemblyName: typeof(DynamicEntityResource).Assembly.GetName().Name,
                menuItems: new List<MenuItem>()));
        }

        protected virtual Task<MenuItem> GenerateEntityMenuItemAsync(ModelDefinitionEto modelDefinition)
        {
            return Task.FromResult(new MenuItem(
                parentName: _menuItemNameCalculator.GetName(ModuleName),
                inAdministration: false,
                name: _menuItemNameCalculator.GetName(ModuleName, modelDefinition.Name),
                displayName: _menuItemNameCalculator.GetDisplayName(ModuleName, modelDefinition.Name),
                url: $"/EntityUi/{ModuleName}/{modelDefinition.Name}",
                urlMvc: null,
                urlBlazor: null,
                urlAngular: $"/entity-ui/{ModuleName}/{modelDefinition.Name.ToKebabCase()}",
                permission: modelDefinition.PermissionSet.GetList,
                order: null,
                icon: null,
                target: null,
                isDisabled: false,
                lResourceTypeName: typeof(DynamicEntityResource).FullName,
                lResourceTypeAssemblyName: typeof(DynamicEntityResource).Assembly.GetName().Name,
                menuItems: new List<MenuItem>()));
        }

        protected virtual Task<EntityDataModel> CreateEntityDataModelAsync(ModelDefinitionEto eto, [CanBeNull] Entity existingEntity)
        {
            const string idPropertyName = "Id";
            var entityName = eto.Name;
            var existingProperties = existingEntity != null ? existingEntity.Properties : new List<Property>();

            foreach (var modelField in eto.Fields.OrderBy(x => x.Order))
            {
                var property = existingProperties.FirstOrDefault(x => x.Name == modelField.FieldDefinition.Name);

                if (property == null)
                {
                    property = new Property(ModuleName, entityName, modelField.FieldDefinition.Name, false,
                        MapFieldDataTypeToCSharpType(modelField.FieldDefinition.Type), true,
                        new PropertyShowInValueObject(true, true, true, true));
                    
                    existingProperties.Add(property);
                }
                else
                {
                    property.Update(ModuleName, entityName, modelField.FieldDefinition.Name, false,
                        MapFieldDataTypeToCSharpType(modelField.FieldDefinition.Type), property.Nullable,
                        property.ShowIn);
                }
            }

            if (!existingProperties.Exists(x => x.Name == idPropertyName))
            {
                existingProperties.AddFirst(new Property(ModuleName, entityName, idPropertyName, false,
                    typeof(Guid).FullName!, false, new PropertyShowInValueObject(false, true, false, false)));
            }

            existingProperties.RemoveAll(x =>
                !eto.Fields.Select(y => y.FieldDefinition.Name).Contains(x.Name) && x.Name != idPropertyName);

            return Task.FromResult(new EntityDataModel(
                moduleName: ModuleName,
                name: entityName,
                providerName: AbpEntityUiDynamicEntityConsts.DynamicEntityEntityProviderName,
                @namespace: Namespace,
                belongsTo: null, // Todo: sub-entities for dynamic entities?
                keys: "Id",
                creationEnabled: true,
                creationPermission: eto.PermissionSet.Create,
                editEnabled: true,
                editPermission: eto.PermissionSet.Update,
                deletionEnabled: true,
                deletionPermission: eto.PermissionSet.Delete,
                detailEnabled: true,
                detailPermission: eto.PermissionSet.Get,
                contractsAssemblyName: "EasyAbp.Abp.DynamicEntity.Application.Contracts",
                listItemDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.DynamicEntityDto",
                detailDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.DynamicEntityDto",
                creationDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.CreateDynamicEntityDto",
                editDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.UpdateDynamicEntityDto",
                getListInputDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.GetListInput",
                keyClassTypeName: null,
                appServiceInterfaceName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.IDynamicEntityAppService",
                appServiceGetListMethodName: "GetListAsync",
                appServiceGetMethodName: "GetAsync",
                appServiceCreateMethodName: "CreateAsync",
                appServiceUpdateMethodName: "UpdateAsync",
                appServiceDeleteMethodName: "DeleteAsync",
                properties: existingProperties));
        }
        
        protected virtual async Task<MenuItem> GetOrCreateModuleMenuItemAsync()
        {
            var moduleMenuItemName = _menuItemNameCalculator.GetName(ModuleName);
            
            var moduleMenuItem = await _menuItemRepository.FindAsync(x => x.Name == moduleMenuItemName);

            if (moduleMenuItem != null)
            {
                return moduleMenuItem;
            }

            moduleMenuItem = await GenerateModuleMenuItemAsync();

            await _menuItemRepository.InsertAsync(moduleMenuItem, true);

            return moduleMenuItem;
        }

        protected virtual string MapFieldDataTypeToCSharpType(FieldDataType fieldDefinitionType)
        {
            return fieldDefinitionType switch
            {
                FieldDataType.Text => typeof(string).FullName,
                FieldDataType.Number => typeof(int).FullName,
                FieldDataType.Float => typeof(float).FullName,
                FieldDataType.Boolean => typeof(bool).FullName,
                FieldDataType.DateTime => typeof(DateTime).FullName,
                _ => throw new ArgumentOutOfRangeException(nameof(fieldDefinitionType), fieldDefinitionType, null)
            };
        }

        public virtual async Task HandleEventAsync(EntityUpdatedEto<ModelDefinitionEto> eventData)
        {
            var entity = await FindEntityByModelDefinitionEtoAsync(eventData.Entity);

            if (entity == null)
            {
                return;
            }

            entity.Update(await CreateEntityDataModelAsync(eventData.Entity, entity));

            await _entityRepository.UpdateAsync(entity, true);
        }

        public virtual async Task HandleEventAsync(EntityDeletedEto<ModelDefinitionEto> eventData)
        {
            var entity = await FindEntityByModelDefinitionEtoAsync(eventData.Entity);

            if (entity == null)
            {
                return;
            }

            var moduleMenuItemName = _menuItemNameCalculator.GetName(ModuleName);
            var entityMenuItemName = _menuItemNameCalculator.GetName(ModuleName, entity.Name);
            
            await _menuItemRepository.DeleteAsync(
                x => x.Name == entityMenuItemName && x.ParentName == moduleMenuItemName, true);

            var moduleMenuItem = await _menuItemRepository.FindAsync(x => x.Name == moduleMenuItemName);

            if (moduleMenuItem != null && !moduleMenuItem.MenuItems.Any())
            {
                await _menuItemRepository.DeleteAsync(moduleMenuItem, true);
            }

            await _entityRepository.DeleteAsync(entity, true);
        }

        protected virtual async Task<Entity> FindEntityByModelDefinitionEtoAsync(ModelDefinitionEto eto)
        {
            return await _entityRepository.FindAsync(x =>
                x.ModuleName == ModuleName && x.Name == eto.Name && x.ProviderName ==
                AbpEntityUiDynamicEntityConsts.DynamicEntityEntityProviderName);
        }
    }
}