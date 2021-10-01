using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.FieldDefinitions;
using EasyAbp.Abp.DynamicEntity.ModelDefinitions;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.MenuItems;
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

        public DynamicEntityModelDefinitionEventHandler(
            IEntityRepository entityRepository,
            IMenuItemRepository menuItemRepository)
        {
            _entityRepository = entityRepository;
            _menuItemRepository = menuItemRepository;
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

            var menuItemName = GetMenuItemName(ModuleName, entityName);

            var menuItem = await _menuItemRepository.FindAsync(x => x.Name == menuItemName);

            if (menuItem == null)
            {
                var localizationItemName = $"Menu:{menuItemName.Split('.').Last()}";

                var moduleMenuItem = await GetOrCreateModuleMenuItemAsync(ModuleName);

                menuItem = new MenuItem(
                    moduleMenuItem.Name,
                    menuItemName,
                    ModuleName,
                    entityName,
                    eventData.Entity.PermissionSet.GetList,
                    localizationItemName,
                    new List<MenuItem>());
                    
                moduleMenuItem.MenuItems.Add(menuItem);
                
                await _menuItemRepository.UpdateAsync(moduleMenuItem, true);
            }
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
                keys: new[] {"Id"},
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

        protected virtual async Task<MenuItem> GetOrCreateModuleMenuItemAsync(string moduleName)
        {
            var moduleMenuItemName = GetMenuItemName(moduleName);
            
            var moduleMenuItem = await _menuItemRepository.FindAsync(x => x.Name == moduleMenuItemName);

            if (moduleMenuItem != null)
            {
                return moduleMenuItem;
            }

            var localizationItemName = $"Menu:{moduleName.Split('.').Last()}";

            return await _menuItemRepository.InsertAsync(
                new MenuItem(null, moduleMenuItemName, moduleName, null, null, localizationItemName,
                    new List<MenuItem>()), true);
        }

        protected virtual string GetMenuItemName(string moduleName)
        {
            return $"{moduleName}";
        }
        
        protected virtual string GetMenuItemName(string moduleName, string aggregateRootName)
        {
            return $"{moduleName}.{aggregateRootName}";
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

            await _menuItemRepository.DeleteAsync(x => x.ModuleName == ModuleName && x.EntityName == entity.Name, true);

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