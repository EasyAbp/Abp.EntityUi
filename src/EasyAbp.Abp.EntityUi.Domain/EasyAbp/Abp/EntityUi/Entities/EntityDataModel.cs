using System.Collections.Generic;
using JetBrains.Annotations;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class EntityDataModel
    {
        public string ModuleName { get; set; }
        
        public string Name { get; set; }
        
        public string ProviderName { get; set; }
        
        public string Namespace { get; set; }
        
        public string BelongsTo { get; set; }

        public string Keys { get; set; }
        
        public bool CreationEnabled { get; set; }
        
        public string CreationPermission { get; set; }
        
        public bool EditEnabled { get; set; }
        
        public string EditPermission { get; set; }
        
        public bool DeletionEnabled { get; set; }
        
        public string DeletionPermission { get; set; }
        
        public bool DetailEnabled { get; set; }
        
        public string DetailPermission { get; set; }
                
        public string ContractsAssemblyName { get; set; }
        
        public string ListItemDtoTypeName { get; set; }
        
        public string DetailDtoTypeName { get; set; }
        
        public string CreationDtoTypeName { get; set; }
        
        public string EditDtoTypeName { get; set; }
        
        public string GetListInputDtoTypeName { get; set; }
        
        public string KeyClassTypeName { get; set; }
                
        public string AppServiceInterfaceName { get; set; }
                
        public string AppServiceGetListMethodName { get; set; }
                
        public string AppServiceGetMethodName { get; set; }
                
        public string AppServiceCreateMethodName { get; set; }
                
        public string AppServiceUpdateMethodName { get; set; }
                
        public string AppServiceDeleteMethodName { get; set; }
        
        public List<Property> Properties { get; set; }
        
        public EntityDataModel([NotNull] string moduleName, [NotNull] string name, [NotNull] string providerName,
            [CanBeNull] string @namespace, [CanBeNull] string belongsTo, [NotNull] string keys, bool creationEnabled,
            [CanBeNull] string creationPermission, bool editEnabled, [CanBeNull] string editPermission,
            bool deletionEnabled, [CanBeNull] string deletionPermission, bool detailEnabled,
            [CanBeNull] string detailPermission, [CanBeNull] string contractsAssemblyName,
            [CanBeNull] string listItemDtoTypeName, [CanBeNull] string detailDtoTypeName,
            [CanBeNull] string creationDtoTypeName, [CanBeNull] string editDtoTypeName,
            [CanBeNull] string getListInputDtoTypeName, [CanBeNull] string keyClassTypeName,
            [CanBeNull] string appServiceInterfaceName, [CanBeNull] string appServiceGetListMethodName,
            [CanBeNull] string appServiceGetMethodName, [CanBeNull] string appServiceCreateMethodName,
            [CanBeNull] string appServiceUpdateMethodName, [CanBeNull] string appServiceDeleteMethodName,
            List<Property> properties)
        {
            ModuleName = moduleName;
            Name = name;
            ProviderName = providerName;
            Namespace = @namespace;
            BelongsTo = belongsTo;
            Keys = keys;
            CreationEnabled = creationEnabled;
            CreationPermission = creationPermission;
            EditEnabled = editEnabled;
            EditPermission = editPermission;
            DeletionEnabled = deletionEnabled;
            DeletionPermission = deletionPermission;
            DetailEnabled = detailEnabled;
            DetailPermission = detailPermission;
            ContractsAssemblyName = contractsAssemblyName;
            ListItemDtoTypeName = listItemDtoTypeName;
            DetailDtoTypeName = detailDtoTypeName;
            CreationDtoTypeName = creationDtoTypeName;
            EditDtoTypeName = editDtoTypeName;
            GetListInputDtoTypeName = getListInputDtoTypeName;
            KeyClassTypeName = keyClassTypeName;
            AppServiceInterfaceName = appServiceInterfaceName;
            AppServiceGetListMethodName = appServiceGetListMethodName;
            AppServiceGetMethodName = appServiceGetMethodName;
            AppServiceCreateMethodName = appServiceCreateMethodName;
            AppServiceUpdateMethodName = appServiceUpdateMethodName;
            AppServiceDeleteMethodName = appServiceDeleteMethodName;

            Properties = properties ?? new List<Property>();
        }
    }
}