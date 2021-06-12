using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.Modules.Dtos
{
    [Serializable]
    public class ModuleDto : EntityDto
    {
        [Required]
        public string Name { get; set; }
        
        public string LResourceTypeName { get; set; }
        
        public string LResourceTypeAssemblyName { get; set; }
    }
}