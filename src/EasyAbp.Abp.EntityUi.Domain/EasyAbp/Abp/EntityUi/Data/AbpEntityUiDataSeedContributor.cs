using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        private static readonly List<string> IgnoreEntityNames = new()
        {
            "AppUser"
        };

        private static readonly List<string> IgnorePropertyNames = new()
        {
            "TenantId", "ConcurrencyStamp", "ExtraProperties"
        };

        private static readonly List<string> AuditPropertyNames = new()
        {
            "IsDeleted", "DeleterId", "DeletionTime", "LastModificationTime", "LastModifierId", "CreationTime",
            "CreatorId"
        };

        private static readonly List<string> ListItemDtoNames = new()
        {
            "{0}Dto", "{0}Model"
        };

        private static readonly List<string> DetailDtoNames = new()
        {
            "{0}Dto", "{0}Model"
        };

        private static readonly List<string> CreateDtoNames = new()
        {
            "CreateUpdate{0}Dto", "Create{0}Dto", "CreateUpdate{0}Input", "Create{0}Input"
        };

        private static readonly List<string> EditDtoNames = new()
        {
            "CreateUpdate{0}Dto", "Update{0}Dto", "CreateUpdate{0}Input", "Update{0}Input",
            "CreateEdit{0}Dto", "Edit{0}Dto", "CreateEdit{0}Input", "Edit{0}Input"
        };

        private static readonly List<string> GetListInputDtoNames = new()
        {
            "Get{0}ListInput", "Get{0}ListDto"
        };

        private static readonly List<string> KeyClassNames = new()
        {
            "{0}Key", "{0}Keys"
        };
        
        private static readonly List<string> AppServiceInterfaceNames = new()
        {
            "I{0}AppService", "I{0}ApplicationService"
        };
        
        private static readonly List<string> AppServiceGetListMethodNames = new()
        {
            "GetListAsync", "GetList"
        };
        
        private static readonly List<string> AppServiceGetMethodNames = new()
        {
            "GetAsync", "Get"
        };
        
        private static readonly List<string> AppServiceCreateMethodNames = new()
        {
            "CreateAsync", "Create"
        };
        
        private static readonly List<string> AppServiceUpdateMethodNames = new()
        {
            "UpdateAsync", "Update", "EditAsync", "Edit"
        };
        
        private static readonly List<string> AppServiceDeleteMethodNames = new()
        {
            "DeleteAsync", "Delete"
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
                    .Where(x => !IgnoreEntityNames.Contains(x.Name))
                    .ToArray();
                
                await GetOrCreateEntitiesAsync(module, entityTypeInfos);

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

            var localizationItemName = $"Menu:{moduleName.Split('.').Last()}";

            return await _menuItemRepository.InsertAsync(
                new MenuItem(null, moduleMenuItemName, moduleName, null, null, localizationItemName,
                    new List<MenuItem>()), true);
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
                    var localizationItemName = $"Menu:{menuItemName.Split('.').Last()}";

                    menuItem = new MenuItem(moduleMenuItem.Name, menuItemName, moduleName, aggregateRoot.Name,
                        GetDefaultMenuItemPermission(moduleName, entityName), localizationItemName,
                        new List<MenuItem>());
                    
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

        protected virtual async Task<List<Entity>> GetOrCreateEntitiesAsync(Module module, TypeInfo[] entityTypeInfos)
        {
            var entities = new List<Entity>();
            var entityNameToParentEntityNameMapping = new Dictionary<string, string>();

            var entityNames = entityTypeInfos.Select(x => x.Name).ToArray();
            
            foreach (var entityTypeInfo in entityTypeInfos)
            {
                foreach (var propertyInfo in entityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !TypeHelper.IsPrimitiveExtended(x.PropertyType, true, true)))
                {
                    if (!TypeHelper.IsEnumerable(propertyInfo.PropertyType, out var baseType, false))
                    {
                        baseType = propertyInfo.PropertyType;
                    }

                    if (entityNames.Contains(baseType.Name))
                    {
                        entityNameToParentEntityNameMapping.AddIfNotContains(
                            new KeyValuePair<string, string>(baseType.Name, entityTypeInfo.Name));
                    }
                }
            }
            
            foreach (var entityTypeInfo in entityTypeInfos)
            {
                var entityName = entityTypeInfo.Name;

                var entity = await _entityRepository.FindAsync(x => x.ModuleName == module.Name && x.Name == entityName);

                if (entity == null)
                {
                    var parentEntityName = entityNameToParentEntityNameMapping.GetOrDefault(entityName);

                    var belongsTo = parentEntityName != entityName ? parentEntityName : null;

                    var keys = typeof(Entity<>).IsAssignableFrom(entityTypeInfo)
                        ? new[] {"Id"}
                        : GetEntityKeys(entityTypeInfo);

                    var parentEntity = parentEntityName != null
                        ? entityTypeInfos.Single(x => x.Name == parentEntityName)
                        : null;

                    var properties = CreateEntityProperties(module.Name, entityName, parentEntity, entityTypeInfo, entityNames);

                    var entityNameForPermission = belongsTo ?? entityName;
                    
                    var contractsAssembly = AppDomain.CurrentDomain.GetAssemblies()
                        .SingleOrDefault(x => x.GetName().Name == $"{module.Name}.Application.Contracts");

                    var listItemDtoType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        ListItemDtoNames.Select(y => string.Format(y, entityName)).Contains(x.Name));

                    var detailDtoType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        DetailDtoNames.Select(y => string.Format(y, entityName)).Contains(x.Name));

                    var creationDtoType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        CreateDtoNames.Select(y => string.Format(y, entityName)).Contains(x.Name));

                    var editDtoType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        EditDtoNames.Select(y => string.Format(y, entityName)).Contains(x.Name));
                    
                    var getListInputDtoType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        GetListInputDtoNames.Select(y => string.Format(y, entityName)).Contains(x.Name));

                    var keyClassType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        KeyClassNames.Select(y => string.Format(y, entityName)).Contains(x.Name));
                    
                    var appServiceInterfaceType = contractsAssembly?.DefinedTypes.FirstOrDefault(x =>
                        AppServiceInterfaceNames.Select(y => string.Format(y, entityName)).Contains(x.Name));

                    var appServiceMethods = appServiceInterfaceType?.ImplementedInterfaces
                        .SelectMany(x => x.GetMethods()).ToList();
                    
                    var appServiceGetListMethod = appServiceMethods?.FirstOrDefault(x =>
                        AppServiceGetListMethodNames.Contains(x.Name) && IsGetListMethod(x, getListInputDtoType));
                    
                    var appServiceGetMethod = appServiceMethods?.FirstOrDefault(x =>
                        AppServiceGetMethodNames.Contains(x.Name) && IsGetMethod(x, keyClassType, keys));
                    
                    var appServiceCreateMethod = appServiceMethods?.FirstOrDefault(x =>
                        AppServiceCreateMethodNames.Contains(x.Name) && IsCreateMethod(x, creationDtoType));
                    
                    var appServiceUpdateMethod = appServiceMethods?.FirstOrDefault(x =>
                        AppServiceUpdateMethodNames.Contains(x.Name) && IsUpdateMethod(x, keyClassType, keys, editDtoType));
                    
                    var appServiceDeleteMethod = appServiceMethods?.FirstOrDefault(x =>
                        AppServiceDeleteMethodNames.Contains(x.Name) && IsDeleteMethod(x, keyClassType, keys));
                    
                    entity = new Entity(
                        moduleName: module.Name,
                        name: entityName,
                        @namespace: entityTypeInfo.Namespace,
                        belongsTo: belongsTo,
                        keys: keys,
                        creationEnabled: creationDtoType != null,
                        creationPermission: GetDefaultCreationPermission(module.Name, entityNameForPermission),
                        editEnabled: editDtoType != null,
                        editPermission: GetDefaultEditPermission(module.Name, entityNameForPermission),
                        deletionEnabled: true,
                        deletionPermission: GetDefaultDeletionPermission(module.Name, entityNameForPermission),
                        detailEnabled: detailDtoType != null,
                        detailPermission: GetDefaultDetailPermission(module.Name, entityNameForPermission),
                        contractsAssemblyName: contractsAssembly?.GetName().Name,
                        listItemDtoTypeName: listItemDtoType?.FullName,
                        detailDtoTypeName: detailDtoType?.FullName,
                        creationDtoTypeName: creationDtoType?.FullName,
                        editDtoTypeName: editDtoType?.FullName,
                        getListInputDtoTypeName: getListInputDtoType?.FullName,
                        keyClassTypeName: keyClassType?.FullName,
                        appServiceInterfaceName: appServiceInterfaceType?.FullName,
                        appServiceGetListMethodName: appServiceGetListMethod?.Name,
                        appServiceGetMethodName: appServiceGetMethod?.Name,
                        appServiceCreateMethodName: appServiceCreateMethod?.Name,
                        appServiceUpdateMethodName: appServiceUpdateMethod?.Name,
                        appServiceDeleteMethodName: appServiceDeleteMethod?.Name,
                        properties: properties);
                    
                    await _entityRepository.InsertAsync(entity, true);
                }
                
                entities.Add(entity);
            }

            return entities;
        }

        protected virtual bool IsDeleteMethod(MethodInfo methodInfo, TypeInfo keyClassType, string[] keys)
        {
            var @params = methodInfo.GetParameters();
            
            if (@params.Length != 1)
            {
                return false;
            }

            return IsKeyParam(@params[0], keyClassType, keys);
        }

        protected virtual bool IsUpdateMethod(MethodInfo methodInfo, TypeInfo keyClassType, string[] keys,
            TypeInfo editDtoType)
        {
            var @params = methodInfo.GetParameters();

            if (@params.Length != 2 || editDtoType == null)
            {
                return false;
            }

            return IsKeyParam(@params[0], keyClassType, keys) && @params[1].ParameterType == editDtoType;
        }

        protected virtual bool IsCreateMethod(MethodInfo methodInfo, TypeInfo creationDtoType)
        {
            var @params = methodInfo.GetParameters();
            
            if (@params.Length != 1 || creationDtoType == null)
            {
                return false;
            }

            return @params[0].ParameterType == creationDtoType;
        }

        protected virtual bool IsGetMethod(MethodInfo methodInfo, TypeInfo keyClassType, string[] keys)
        {
            var @params = methodInfo.GetParameters();
            
            if (@params.Length != 1)
            {
                return false;
            }

            return IsKeyParam(@params[0], keyClassType, keys);
        }

        protected virtual bool IsKeyParam(ParameterInfo parameterInfo, TypeInfo keyClassType, string[] keys)
        {
            if (keyClassType == null)
            {
                if (keys.Length != 1)
                {
                    return false;
                }
                
                return parameterInfo.Name.Equals(keys.First(), StringComparison.OrdinalIgnoreCase);
            }

            return parameterInfo.ParameterType == keyClassType;
        }

        protected virtual bool IsGetListMethod(MethodInfo methodInfo, TypeInfo getListInputDtoType)
        {
            var @params = methodInfo.GetParameters();
            
            if (@params.Length != 1)
            {
                return false;
            }

            if (getListInputDtoType == null)
            {
                return @params[0].ParameterType.FullName == "Volo.Abp.Application.Dtos.PagedAndSortedResultRequestDto";
            }

            return @params[0].ParameterType == getListInputDtoType;
        }

        protected virtual List<Property> CreateEntityProperties(string moduleName, string entityName,
            TypeInfo parentEntityTypeInfo, TypeInfo entityTypeInfo, string[] entityNames)
        {
            var properties = new List<Property>();

            var foreignKeys = GetForeignKeys(entityTypeInfo, parentEntityTypeInfo);

            foreach (var propertyInfo in entityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                var propertyName = propertyInfo.Name;

                if (IgnorePropertyNames.Contains(propertyName))
                {
                    continue;
                }
                
                var isForeignKey = IsForeignKeyProperty(propertyName, parentEntityTypeInfo, foreignKeys);

                var isAuditProperty = AuditPropertyNames.Contains(propertyName);

                var isPrimitiveType = TypeHelper.IsPrimitiveExtended(propertyInfo.PropertyType, includeEnums: true);

                var isNullable = TypeHelper.IsNullable(propertyInfo.PropertyType);

                var isEntityCollection = TypeHelper.IsEnumerable(propertyInfo.PropertyType, out var baseType, false) &&
                                         !TypeHelper.IsPrimitiveExtended(baseType, includeEnums: true);
                
                if (!isEntityCollection)
                {
                    baseType = propertyInfo.PropertyType.GetFirstGenericArgumentIfNullable();
                }

                var isEntity = entityNames.Contains(baseType.Name);

                var property = new Property(moduleName, entityName, propertyName, isEntityCollection,
                    baseType.FullName, isNullable,
                    new PropertyShowInValueObject(
                        !isEntity && !isAuditProperty && !isForeignKey && isPrimitiveType,
                        !isEntity,
                        !isAuditProperty && !isForeignKey,
                        !isAuditProperty && !isForeignKey));

                properties.Add(property);
            }

            return properties;
        }

        protected virtual bool IsForeignKeyProperty(string propertyName, TypeInfo parentEntityTypeInfo, IEnumerable<string> foreignKeys)
        {
            if (parentEntityTypeInfo == null)
            {
                return false;
            }

            return propertyName == $"{parentEntityTypeInfo.Name}Id" || foreignKeys.Contains(propertyName);
        }

        protected virtual List<string> GetForeignKeys(TypeInfo entityTypeInfo, TypeInfo parentEntityTypeInfo)
        {
            return GetForeignKeysFromEntityTypeInfo(entityTypeInfo)
                .Union(GetForeignKeysFromParentEntityTypeInfo(entityTypeInfo, parentEntityTypeInfo)).ToList();
        }

        protected virtual List<string> GetForeignKeysFromEntityTypeInfo(TypeInfo entityTypeInfo)
        {
            return entityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => Attribute.IsDefined(x, typeof(ForeignKeyAttribute))).Select(x => x.Name).ToList();
        }
        
        protected virtual List<string> GetForeignKeysFromParentEntityTypeInfo(TypeInfo entityTypeInfo, TypeInfo parentEntityTypeInfo)
        {
            if (parentEntityTypeInfo == null)
            {
                return new List<string>();
            }
            
            return parentEntityTypeInfo.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(x => IsRelationshipProperty(x, entityTypeInfo.Name))
                .Where(x => Attribute.IsDefined(x, typeof(ForeignKeyAttribute))).SelectMany(x =>
                    x.GetCustomAttribute<ForeignKeyAttribute>().Name
                        .Split(new[] {",", " "}, StringSplitOptions.RemoveEmptyEntries)).ToList();
        }

        protected virtual bool IsRelationshipProperty(PropertyInfo propertyInfo, string entityName)
        {
            var isEntityCollection = TypeHelper.IsEnumerable(propertyInfo.PropertyType, out var baseType, false) &&
                                     !TypeHelper.IsPrimitiveExtended(baseType, includeEnums: true);

            if (!isEntityCollection)
            {
                baseType = propertyInfo.PropertyType.GetFirstGenericArgumentIfNullable();
            }

            return entityName == baseType.Name;
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