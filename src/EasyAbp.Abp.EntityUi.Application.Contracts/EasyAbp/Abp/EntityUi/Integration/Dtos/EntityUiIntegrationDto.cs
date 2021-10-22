using System;
using System.Collections.Generic;
using EasyAbp.Abp.DynamicMenu.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Integration.Dtos
{
    [Serializable]
    public class EntityUiIntegrationDto
    {
        public List<ModuleDto> Modules { get; set; }
        
        public List<EntityDto> Entities { get; set; }
        
        public List<MenuItemDto> MenuItems { get; set; }
    }
}