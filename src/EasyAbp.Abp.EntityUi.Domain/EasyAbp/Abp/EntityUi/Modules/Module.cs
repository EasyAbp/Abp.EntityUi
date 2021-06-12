using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class Module : AggregateRoot
    {
        [Key]
        [NotNull]
        public virtual string Name { get; protected set; }
        
        [CanBeNull]
        public virtual string LResourceTypeName { get; protected set; }
        
        [CanBeNull]
        public virtual string LResourceTypeAssemblyName { get; protected set; }

        protected Module()
        {
        }

        public Module(
            [NotNull] string name,
            [CanBeNull] string lResourceTypeName,
            [CanBeNull] string lResourceTypeAssemblyName)
        {
            Name = name;
            LResourceTypeName = lResourceTypeName;
            LResourceTypeAssemblyName = lResourceTypeAssemblyName;
        }

        public override object[] GetKeys()
        {
            return new object[] {Name};
        }
    }
}