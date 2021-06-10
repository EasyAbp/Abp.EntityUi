using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class CreateUpdateEntityDto
    {
        [DisplayName("EntityModuleName")]
        public string ModuleName { get; set; }

        [DisplayName("EntityName")]
        public string Name { get; set; }

        [DisplayName("EntityBelongsTo")]
        public string BelongsTo { get; set; }

        [DisplayName("EntityKeys")]
        public string[] Keys { get; set; }

        [DisplayName("EntityCreationEnabled")]
        public bool CreationEnabled { get; set; }

        [DisplayName("EntityCreationPermission")]
        public string CreationPermission { get; set; }

        [DisplayName("EntityEditEnabled")]
        public bool EditEnabled { get; set; }

        [DisplayName("EntityEditPermission")]
        public string EditPermission { get; set; }

        [DisplayName("EntityDeletionEnabled")]
        public bool DeletionEnabled { get; set; }

        [DisplayName("EntityDeletionPermission")]
        public string DeletionPermission { get; set; }

        [DisplayName("EntityDetailEnabled")]
        public bool DetailEnabled { get; set; }

        [DisplayName("EntityDetailPermission")]
        public string DetailPermission { get; set; }

        [DisplayName("EntityProperties")]
        public List<PropertyDto> Properties { get; set; }
    }
}