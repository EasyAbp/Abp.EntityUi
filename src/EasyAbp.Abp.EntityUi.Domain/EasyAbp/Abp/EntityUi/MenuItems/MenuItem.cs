using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public class MenuItem : AggregateRoot
    {
        [CanBeNull]
        public virtual string ParentName { get; protected set; }

        [Key]
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [CanBeNull]
        public virtual string EntityModuleName { get; protected set; }
        
        [CanBeNull]
        public virtual string EntityName { get; protected set; }
        
        [CanBeNull]
        public virtual string Permission { get; protected set; }
        
        [ForeignKey(nameof(ParentName))]
        public virtual List<MenuItem> MenuItems { get; protected set; }

        protected MenuItem()
        {
            MenuItems = new List<MenuItem>();
        }

        public MenuItem([CanBeNull] string parentName, [NotNull] string name, [CanBeNull] string entityModuleName,
            [CanBeNull] string entityName, [CanBeNull] string permission, List<MenuItem> menuItems)
        {
            ParentName = parentName;
            Name = name;
            EntityModuleName = entityModuleName;
            EntityName = entityName;
            Permission = permission;

            MenuItems = menuItems ?? new List<MenuItem>();
        }

        public override object[] GetKeys()
        {
            return new object[] {Name};
        }
    }
}