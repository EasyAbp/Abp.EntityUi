using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class Entity : AggregateRoot
    {
        [Key, Column(Order = 0)]
        [NotNull]
        public virtual string ModuleName { get; protected set; }
        
        [Key, Column(Order = 1)]
        [NotNull]
        public virtual string Name { get; protected set; }
        
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

        public Entity([NotNull] string moduleName, [NotNull] string name, [CanBeNull] string @namespace,
            [CanBeNull] string belongsTo, [NotNull] string[] keys, bool creationEnabled,
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

        public override object[] GetKeys()
        {
            return new object[] {ModuleName, Name};
        }
    }
}