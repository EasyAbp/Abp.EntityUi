using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.MenuItems.Dtos
{
    [Serializable]
    public class MenuItemDto : EntityDto
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string ModuleName { get; set; }

        public string EntityName { get; set; }

        public string Permission { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }
    }
}