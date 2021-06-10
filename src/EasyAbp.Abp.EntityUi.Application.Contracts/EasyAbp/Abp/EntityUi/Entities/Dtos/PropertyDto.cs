using System;

namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class PropertyDto : Volo.Abp.Application.Dtos.EntityDto
    {
        public string EntityModuleName { get; set; }

        public string EntityName { get; set; }

        public string Name { get; set; }

        public bool IsEntityCollection { get; set; }

        public string TypeOrEntityName { get; set; }

        public PropertyShowInDto ShowIn { get; set; }
    }
}