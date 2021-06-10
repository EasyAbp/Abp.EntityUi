using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class Module : AuditedEntity
    {
        [Key]
        [NotNull]
        public virtual string Name { get; protected set; }

        protected Module()
        {
        }
        
        public Module([NotNull] string name)
        {
            Name = name;
        }
        
        public override object[] GetKeys()
        {
            return new object[] {Name};
        }
    }
}