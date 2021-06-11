using System;
using System.Collections.Generic;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.Modules;

namespace EasyAbp.Abp.EntityUi.Integration
{
    [Serializable]
    public class EntityUiIntegrationModel
    {
        public List<Module> Modules { get; set; }
        
        public List<MenuItem> MenuItems { get; set; }
        
        public List<Entity> Entities { get; set; }
    }
}