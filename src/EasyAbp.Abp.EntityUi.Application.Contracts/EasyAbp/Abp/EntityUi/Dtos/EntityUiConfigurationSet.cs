using System;
using System.Collections.Generic;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Dtos
{
    [Serializable]
    public class EntityUiConfigurationSet
    {
        public List<ModuleDto> Modules { get; set; }
        
        public List<MenuItemDto> MenuItems { get; set; }
        
        public List<EntityDto> Entities { get; set; }
    }
}