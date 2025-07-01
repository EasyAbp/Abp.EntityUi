using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class PropertyDto : Volo.Abp.Application.Dtos.EntityDto
    {
        public string EntityModuleName { get; set; }

        public string EntityName { get; set; }

        public string Name { get; set; }

        public bool IsEntityCollection { get; set; }

        [NotNull] public string TypeOrEntityName { get; set; } = null!;
        
        public bool Nullable { get; set; }

        public PropertyShowInDto ShowIn { get; set; }
    }
}