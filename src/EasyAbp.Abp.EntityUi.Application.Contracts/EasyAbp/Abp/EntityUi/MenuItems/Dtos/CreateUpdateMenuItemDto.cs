using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace EasyAbp.Abp.EntityUi.MenuItems.Dtos
{
    [Serializable]
    public class CreateUpdateMenuItemDto
    {
        [DisplayName("MenuItemParentName")]
        public string ParentName { get; set; }
        
        [DisplayName("MenuItemName")]
        public string Name { get; set; }

        [DisplayName("MenuItemEntityModuleName")]
        public string EntityModuleName { get; set; }

        [DisplayName("MenuItemEntityName")]
        public string EntityName { get; set; }

        [DisplayName("MenuItemPermission")]
        public string Permission { get; set; }

        [DisplayName("MenuItemMenuItems")]
        public List<CreateUpdateMenuItemDto> MenuItems { get; set; }
    }
}