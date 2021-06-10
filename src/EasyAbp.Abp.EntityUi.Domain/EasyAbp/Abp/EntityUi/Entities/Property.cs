using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class Property : Volo.Abp.Domain.Entities.Entity
    {
        [Key, Column(Order = 0)]
        [NotNull]
        public virtual string EntityModuleName { get; protected set; }
        
        [Key, Column(Order = 1)]
        [NotNull]
        public virtual string EntityName { get; protected set; }
        
        [Key, Column(Order = 2)]
        [NotNull]
        public virtual string Name { get; protected set; }
        
        /// <summary>
        /// Set as a sub-entity collection property.
        /// </summary>
        public virtual bool IsEntityCollection { get; protected set; }
        
        /// <summary>
        /// This will be used as the entity name if the IsEntityCollection is true.
        /// </summary>
        [NotNull]
        public virtual string TypeOrEntityName { get; protected set; }
        
        public virtual PropertyShowInValueObject ShowIn { get; protected set; }

        internal Property()
        {
        }
        
        internal Property(
            [NotNull] string entityModuleName,
            [NotNull] string entityName,
            [NotNull] string name,
            PropertyShowInValueObject showIn)
        {
            EntityModuleName = entityModuleName;
            EntityName = entityName;
            Name = name;
            ShowIn = showIn;
        }

        public override object[] GetKeys()
        {
            return new object[] {EntityModuleName, EntityName, Name};
        }
    }
}