using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.FieldDefinitions;
using EasyAbp.Abp.DynamicEntity.ModelDefinitions;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.MenuItems;
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
        ITransientDependency
    {
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
            const string moduleName = "EasyAbp.Abp.DynamicEntity";
            const string @namespace = "EasyAbp.Abp.DynamicEntity.DynamicEntities";
            var entityName = eventData.Entity.Name;

            var entity = await _entityRepository.FindAsync(x => x.Name == entityName);

            if (entity == null)
            {
                var properties = eventData.Entity.Fields.OrderBy(x => x.Order).Select(x =>
                    new Property(moduleName, entityName, x.FieldDefinition.Name, false,
                        MapFieldDataTypeToCSharpType(x.FieldDefinition.Type), true,
                        new PropertyShowInValueObject(true, true, true, true))).ToList();

                entity = new Entity(
                    moduleName: moduleName,
                    name: entityName,
                    providerName: AbpEntityUiDynamicEntityConsts.DynamicEntityEntityProviderName,
                    @namespace: @namespace,
                    belongsTo: null, // Todo: sub-entities for dynamic entities?
                    keys: new[] {"Id"},
                    creationEnabled: true,
                    creationPermission:
                    "EasyAbp.Abp.DynamicEntity.DynamicEntity.Create", // Todo: should be customizable.
                    editEnabled: true,
                    editPermission: "EasyAbp.Abp.DynamicEntity.DynamicEntity.Update", // Todo: should be customizable.
                    deletionEnabled: true,
                    deletionPermission:
                    "EasyAbp.Abp.DynamicEntity.DynamicEntity.Delete", // Todo: should be customizable.
                    detailEnabled: true,
                    detailPermission: "EasyAbp.Abp.DynamicEntity.DynamicEntity", // Todo: should be customizable.
                    contractsAssemblyName: "EasyAbp.Abp.DynamicEntity.Application.Contracts",
                    listItemDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.DynamicEntityDto",
                    detailDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.DynamicEntityDto",
                    creationDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.CreateUpdateDynamicEntityDto",
                    editDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.CreateUpdateDynamicEntityDto",
                    getListInputDtoTypeName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos.GetListInput",
                    keyClassTypeName: null,
                    appServiceInterfaceName: "EasyAbp.Abp.DynamicEntity.DynamicEntities.IDynamicEntityAppService",
                    appServiceGetListMethodName: "GetListAsync",
                    appServiceGetMethodName: "GetAsync",
                    appServiceCreateMethodName: "CreateAsync",
                    appServiceUpdateMethodName: "UpdateAsync",
                    appServiceDeleteMethodName: "DeleteAsync",
                    properties: properties);

                entity.SetProperty(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName,
                    eventData.Entity.Id);

                await _entityRepository.InsertAsync(entity, true);
            }

            var menuItemName = GetMenuItemName(moduleName, entityName);

            var menuItem = await _menuItemRepository.FindAsync(x => x.Name == menuItemName);

            if (menuItem == null)
            {
                var localizationItemName = $"Menu:{menuItemName.Split('.').Last()}";

                var moduleMenuItem = await GetOrCreateModuleMenuItemAsync(moduleName);

                menuItem = new MenuItem(
                    moduleMenuItem.Name,
                    menuItemName, moduleName,
                    entityName,
                    "EasyAbp.Abp.DynamicEntity.DynamicEntity", // Todo: should be customizable.
                    localizationItemName,
                    new List<MenuItem>());
                    
                moduleMenuItem.MenuItems.Add(menuItem);
                
                await _menuItemRepository.UpdateAsync(moduleMenuItem, true);
            }
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
    }
}