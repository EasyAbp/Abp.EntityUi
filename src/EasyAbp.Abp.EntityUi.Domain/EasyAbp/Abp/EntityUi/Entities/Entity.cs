using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class Entity : AuditedAggregateRoot
    {
        [Key, Column(Order = 0)]
        [NotNull]
        public virtual string ModuleName { get; protected set; }
        
        [Key, Column(Order = 1)]
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [NotNull]
        public virtual string ProviderName { get; protected set; }
        
        [CanBeNull]
        public virtual string Namespace { get; protected set; }
        
        /// <summary>
        /// As a sub-entity of specified aggregate root.
        /// </summary>
        [CanBeNull]
        public virtual string BelongsTo { get; protected set; }

        /// <summary>
        /// Will be mapped to "{key1},{key2},{key3}"
        /// </summary>
        [NotNull]
        public virtual string[] Keys { get; protected set; }
        
        public virtual bool CreationEnabled { get; protected set; }
        
        [CanBeNull]
        public virtual string CreationPermission { get; protected set; }
        
        public virtual bool EditEnabled { get; protected set; }
        
        [CanBeNull]
        public virtual string EditPermission { get; protected set; }
        
        public virtual bool DeletionEnabled { get; protected set; }
        
        [CanBeNull]
        public virtual string DeletionPermission { get; protected set; }
        
        public virtual bool DetailEnabled { get; protected set; }
        
        [CanBeNull]
        public virtual string DetailPermission { get; protected set; }
                
        [CanBeNull]
        public virtual string ContractsAssemblyName { get; protected set; }
        
        [CanBeNull]
        public virtual string ListItemDtoTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string DetailDtoTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string CreationDtoTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string EditDtoTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string GetListInputDtoTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string KeyClassTypeName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceInterfaceName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceGetListMethodName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceGetMethodName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceCreateMethodName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceUpdateMethodName { get; protected set; }
                
        [CanBeNull]
        public virtual string AppServiceDeleteMethodName { get; protected set; }
        
        [ForeignKey("EntityModuleName, EntityName")]
        public virtual List<Property> Properties { get; protected set; }

        protected Entity()
        {
            Properties = new List<Property>();
        }

        [JsonConstructor]
        public Entity([NotNull] string moduleName, [NotNull] string name, [NotNull] string providerName,
            [CanBeNull] string @namespace, [CanBeNull] string belongsTo, [NotNull] string[] keys, bool creationEnabled,
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
            Update(new EntityDataModel(moduleName, name, providerName, @namespace, belongsTo, keys, creationEnabled,
                creationPermission, editEnabled, editPermission, deletionEnabled, deletionPermission, detailEnabled,
                detailPermission, contractsAssemblyName, listItemDtoTypeName, detailDtoTypeName, creationDtoTypeName,
                editDtoTypeName, getListInputDtoTypeName, keyClassTypeName, appServiceInterfaceName,
                appServiceGetListMethodName, appServiceGetMethodName, appServiceCreateMethodName,
                appServiceUpdateMethodName, appServiceDeleteMethodName, properties));
        }

        public Entity(EntityDataModel model)
        {
            Update(model);
        }

        public void Update(EntityDataModel model)
        {
            ModuleName = model.ModuleName;
            Name = model.Name;
            ProviderName = model.ProviderName;
            Namespace = model.Namespace;
            BelongsTo = model.BelongsTo;
            Keys = model.Keys;
            CreationEnabled = model.CreationEnabled;
            CreationPermission = model.CreationPermission;
            EditEnabled = model.EditEnabled;
            EditPermission = model.EditPermission;
            DeletionEnabled = model.DeletionEnabled;
            DeletionPermission = model.DeletionPermission;
            DetailEnabled = model.DetailEnabled;
            DetailPermission = model.DetailPermission;
            ContractsAssemblyName = model.ContractsAssemblyName;
            ListItemDtoTypeName = model.ListItemDtoTypeName;
            DetailDtoTypeName = model.DetailDtoTypeName;
            CreationDtoTypeName = model.CreationDtoTypeName;
            EditDtoTypeName = model.EditDtoTypeName;
            GetListInputDtoTypeName = model.GetListInputDtoTypeName;
            KeyClassTypeName = model.KeyClassTypeName;
            AppServiceInterfaceName = model.AppServiceInterfaceName;
            AppServiceGetListMethodName = model.AppServiceGetListMethodName;
            AppServiceGetMethodName = model.AppServiceGetMethodName;
            AppServiceCreateMethodName = model.AppServiceCreateMethodName;
            AppServiceUpdateMethodName = model.AppServiceUpdateMethodName;
            AppServiceDeleteMethodName = model.AppServiceDeleteMethodName;

            Properties = model.Properties ?? new List<Property>();
        }

        public override object[] GetKeys()
        {
            return new object[] {ModuleName, Name};
        }
    }
}