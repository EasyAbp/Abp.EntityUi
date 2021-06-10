using System;
using System.ComponentModel;
namespace EasyAbp.Abp.EntityUi.Modules.Dtos
{
    [Serializable]
    public class CreateUpdateModuleDto
    {
        [DisplayName("ModuleName")]
        public string Name { get; set; }
    }
}