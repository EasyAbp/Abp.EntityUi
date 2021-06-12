using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class EntityDto : Volo.Abp.Application.Dtos.EntityDto
    {
        public string ModuleName { get; set; }

        public string Name { get; set; }
        
        public string Namespace { get; set; }

        public string BelongsTo { get; set; }

        public string[] Keys { get; set; }

        public bool CreationEnabled { get; set; }

        public string CreationPermission { get; set; }

        public bool EditEnabled { get; set; }

        public string EditPermission { get; set; }

        public bool DeletionEnabled { get; set; }

        public string DeletionPermission { get; set; }

        public bool DetailEnabled { get; set; }

        public string DetailPermission { get; set; }

        public List<PropertyDto> Properties { get; set; }
    }
}