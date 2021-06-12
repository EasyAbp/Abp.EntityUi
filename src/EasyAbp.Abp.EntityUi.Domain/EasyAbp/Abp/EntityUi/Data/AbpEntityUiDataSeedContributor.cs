using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Options;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Json;
using Volo.Abp.Localization;
using Volo.Abp.Reflection;
using Volo.Abp.VirtualFileSystem;
using Entity = EasyAbp.Abp.EntityUi.Entities.Entity;
using Module = EasyAbp.Abp.EntityUi.Modules.Module;

namespace EasyAbp.Abp.EntityUi.Data
{
    public class AbpEntityUiDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private static readonly string[] IgnorePropertyNames = {"TenantId", "ConcurrencyStamp", "ExtraProperties"};

        private static readonly string[] AuditPropertyNames =
        {
            "IsDeleted", "DeleterId", "DeletionTime", "LastModificationTime", "LastModifierId", "CreationTime",
            "CreatorId"
        };

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IVirtualFileProvider _virtualFileProvider;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly AbpEntityUiOptions _options;

        public AbpEntityUiDataSeedContributor(
            IOptions<AbpEntityUiOptions> options,
            IJsonSerializer jsonSerializer,
            IVirtualFileProvider virtualFileProvider,
            IMenuItemRepository menuItemRepository,
            IEntityRepository entityRepository,
            IModuleRepository moduleRepository)
        {
            _jsonSerializer = jsonSerializer;
            _virtualFileProvider = virtualFileProvider;
            _menuItemRepository = menuItemRepository;
            _entityRepository = entityRepository;
            _moduleRepository = moduleRepository;
            _options = options.Value;
        }
        
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await TrySeedByJsonFileAsync();
            
            await TrySeedByReflectionAsync();
        }

        protected virtual async Task TrySeedByJsonFileAsync()
        {
            foreach (var pair in _options.Modules.Where(x => !x.Value.SeedJsonFilePath.IsNullOrWhiteSpace()))
            {
                var file = _virtualFileProvider.GetFileInfo(pair.Value.SeedJsonFilePath);

                if (!file.Exists)
                {
                    return;
                }
            
                var fileContent = await file.ReadAsStringAsync();

                var integrationModel = _jsonSerializer.Deserialize<EntityUiIntegrationModel>(fileContent);

                if (integrationModel == null)
                {
                    return;
                }
            
                foreach (var module in integrationModel.Modules ?? new List<Module>())
                {
                    if (await _moduleRepository.FindAsync(x => x.Name == module.Name) == null)
                    {
                        await _moduleRepository.InsertAsync(module, true);
                    }
                }

                foreach (var entity in integrationModel.Entities ?? new List<Entity>())
                {
                    if (await _entityRepository.FindAsync(x =>
                        x.ModuleName == entity.ModuleName && x.Name == entity.Name) == null)
                    {
                        await _entityRepository.InsertAsync(entity, true);
                    }
                }
            
                foreach (var menuItem in integrationModel.MenuItems ?? new List<MenuItem>())
                {
                    if (await _menuItemRepository.FindAsync(x => x.Name == menuItem.Name) == null)
                    {
                        await _menuItemRepository.InsertAsync(menuItem, true);
                    }
                }
            }
        }

        protected virtual async Task TrySeedByReflectionAsync()
        {
            foreach (var pair in _options.Modules)
            {
                var module = await GetOrCreateModuleAsync(pair.Key);

                var entityTypeInfos = pair.Value.AbpModuleType.Assembly.DefinedTypes
                    .Where(typeof(Volo.Abp.Domain.Entities.Entity).IsAssignableFrom).Where(x => !x.IsAbstract)
                    .ToArray();
                
                await GetOrCreateEntitiesAsync(module.Name, entityTypeInfos);

                var moduleMenuItem = await GetOrCreateModuleMenuItemAsync(module.Name);
                await TryCreateEntityMenuItemsAsync(module.Name, entityTypeInfos, moduleMenuItem);
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

            return await _menuItemRepository.InsertAsync(
                new MenuItem(null, moduleMenuItemName, moduleName, null, null, new List<MenuItem>()), true);
        }

        protected virtual async Task TryCreateEntityMenuItemsAsync(string moduleName, TypeInfo[] entityTypeInfos, MenuItem moduleMenuItem)
        {
            var aggregateRoots = entityTypeInfos.Where(typeof(IAggregateRoot).IsAssignableFrom).ToArray();

            foreach (var aggregateRoot in aggregateRoots)
            {
                var entityName = aggregateRoot.Name;
                
                var menuItemName = GetMenuItemName(moduleName, entityName);
                
                var menuItem = await _menuItemRepository.FindAsync(x => x.Name == menuItemName);

                if (menuItem == null)
                {
                    menuItem = new MenuItem(moduleMenuItem.Name, menuItemName, moduleName, aggregateRoot.Name,
                        GetDefaultMenuItemPermission(moduleName, entityName), new List<MenuItem>());
                    
                    moduleMenuItem.MenuItems.Add(menuItem);
                }
            }

            await _menuItemRepository.UpdateAsync(moduleMenuItem);
        }

        protected virtual string GetMenuItemName(string moduleName)
        {
            return $"{moduleName}";
        }


        protected virtual string GetMenuItemName(string moduleName, string aggregateRootName)
        {
            return $"{moduleName}.{aggregateRootName}";
        }

        protected virtual async Task<Module> GetOrCreateModuleAsync(string moduleName)
        {
            var module = await _moduleRepository.FindAsync(x => x.Name == moduleName);

            return module ?? await CreateModuleAsync(moduleName);
        }

        protected virtual async Task<Module> CreateModuleAsync(string moduleName)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(x => x.GetName().Name == $"{moduleName}.Domain.Shared");
            
            var resourceType = assembly?.DefinedTypes
                .FirstOrDefault(x => Attribute.IsDefined(x, typeof(LocalizationResourceNameAttribute)));

            if (resourceType == null)
            {
                return await _moduleRepository.InsertAsync(new Module(moduleName, null, null), true);
            }

            var resourceTypeName = resourceType.FullName;
            var assemblyName = resourceType.Assembly.GetName().Name;

            return await _moduleRepository.InsertAsync(new Module(moduleName, resourceTypeName, assemblyName), true);
        }

        protected virtual async Task<List<Entity>> GetOrCreateEntitiesAsync(string moduleName, TypeInfo[] entityTypeInfos)
        {
            var entities = new List<Entity>();
            var entityBelongsToEntityMapping = new Dictionary<string, string>();

            var entityNames = entityTypeInfos.Select(x => x.Name).ToArray();
            
            foreach (var entityTypeInfo in entityTypeInfos)
            {
                foreach (var propertyInfo in entityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !TypeHelper.IsPrimitiveExtended(x.PropertyType, true, true)))
                {
                    if (!TypeHelper.IsEnumerable(propertyInfo.PropertyType, out var baseType, false))
                    {
                        baseType = propertyInfo.PropertyType;
                    }

                    if (baseType.Name != entityTypeInfo.Name && entityNames.Contains(baseType.Name))
                    {
                        entityBelongsToEntityMapping.AddIfNotContains(
                            new KeyValuePair<string, string>(baseType.Name, entityTypeInfo.Name));
                    }
                }
            }
            
            foreach (var entityTypeInfo in entityTypeInfos)
            {
                var entityName = entityTypeInfo.Name;

                var entity = await _entityRepository.FindAsync(x => x.ModuleName == moduleName && x.Name == entityName);

                if (entity == null)
                {
                    var belongsTo = entityBelongsToEntityMapping.GetOrDefault(entityName);

                    var keys = typeof(Entity<>).IsAssignableFrom(entityTypeInfo)
                        ? new[] {"Id"}
                        : GetEntityKeys(entityTypeInfo);

                    var properties = CreateEntityProperties(moduleName, entityName, entityTypeInfo, entityNames);

                    var entityNameForPermission = belongsTo ?? entityName;

                    entity = new Entity(
                        moduleName: moduleName,
                        name: entityName,
                        @namespace: entityTypeInfo.Namespace,
                        belongsTo: belongsTo,
                        keys: keys,
                        creationEnabled: true,
                        creationPermission: GetDefaultCreationPermission(moduleName, entityNameForPermission),
                        editEnabled: true,
                        editPermission: GetDefaultEditPermission(moduleName, entityNameForPermission),
                        deletionEnabled: true,
                        deletionPermission: GetDefaultDeletionPermission(moduleName, entityNameForPermission),
                        detailEnabled: true,
                        detailPermission: GetDefaultDetailPermission(moduleName, entityNameForPermission),
                        properties: properties);
                    
                    await _entityRepository.InsertAsync(entity, true);
                }
                
                entities.Add(entity);
            }

            return entities;
        }

        protected virtual List<Property> CreateEntityProperties(string moduleName, string entityName,
            TypeInfo entityTypeInfo, string[] entityNames)
        {
            var properties = new List<Property>();
            
            foreach (var propertyInfo in entityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyName = propertyInfo.Name;

                if (IgnorePropertyNames.Contains(propertyName))
                {
                    continue;
                }
                
                var isAuditProperty = AuditPropertyNames.Contains(propertyName);

                var isNullable = TypeHelper.IsNullable(propertyInfo.PropertyType);

                var isEntityCollection = TypeHelper.IsEnumerable(propertyInfo.PropertyType, out var baseType, false) &&
                                         !TypeHelper.IsPrimitiveExtended(baseType, includeEnums: true);

                if (!isEntityCollection)
                {
                    baseType = propertyInfo.PropertyType.GetFirstGenericArgumentIfNullable();
                }

                var isEntity = entityNames.Contains(baseType.Name);

                var property = new Property(moduleName, entityName, propertyName, isEntityCollection,
                    baseType.Name, isNullable,
                    new PropertyShowInValueObject(
                        !isEntity && !isAuditProperty,
                        !isEntity,
                        !isEntity && !isAuditProperty,
                        !isEntity && !isAuditProperty));
                
                properties.Add(property);
            }

            return properties;
        }

        protected virtual string GetDefaultCreationPermission(string moduleName, string entityName)
        {
            return $"{moduleName}.{entityName}.Create";
        }
        
        protected virtual string GetDefaultEditPermission(string moduleName, string entityName)
        {
            return $"{moduleName}.{entityName}.Update";
        }
        
        protected virtual string GetDefaultDeletionPermission(string moduleName, string entityName)
        {
            return $"{moduleName}.{entityName}.Delete";
        }
        
        protected virtual string GetDefaultDetailPermission(string moduleName, string entityName)
        {
            return $"{moduleName}.{entityName}";
        }
        
        protected virtual string GetDefaultMenuItemPermission(string moduleName, string entityName)
        {
            return $"{moduleName}.{entityName}";
        }

        protected virtual string[] GetEntityKeys(TypeInfo entityTypeInfo)
        {
            // Todo: Get key properties from GetKeys() method.
            return new[] {"Id"};
        }
    }
}