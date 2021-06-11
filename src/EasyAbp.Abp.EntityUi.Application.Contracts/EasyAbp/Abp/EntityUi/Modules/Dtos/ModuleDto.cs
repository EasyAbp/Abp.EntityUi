using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.Modules.Dtos
{
    [Serializable]
    public class ModuleDto : EntityDto
    {
        public string Name { get; set; }
    }
}