using System;
using System.ComponentModel;
namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class CreateUpdatePropertyDto
    {
        [DisplayName("PropertyEntityModuleName")]
        public string EntityModuleName { get; set; }

        [DisplayName("PropertyEntityName")]
        public string EntityName { get; set; }

        [DisplayName("PropertyName")]
        public string Name { get; set; }

        [DisplayName("PropertyIsEntityCollection")]
        public bool IsEntityCollection { get; set; }

        [DisplayName("PropertyTypeOrEntityName")]
        public string TypeOrEntityName { get; set; }

        [DisplayName("PropertyShowIn")]
        public PropertyShowInDto ShowIn { get; set; }
    }
}