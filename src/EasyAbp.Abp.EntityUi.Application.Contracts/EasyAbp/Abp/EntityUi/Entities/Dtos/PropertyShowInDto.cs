using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class PropertyShowInDto : IPropertyShowIn
    {
        [Display(Name = "PropertyShowInList")]
        public bool List { get; set; }
        
        [Display(Name = "PropertyShowInDetail")]
        public bool Detail { get; set; }
        
        [Display(Name = "PropertyShowInCreation")]
        public bool Creation { get; set; }
        
        [Display(Name = "PropertyShowInEdit")]
        public bool Edit { get; set; }
    }
}