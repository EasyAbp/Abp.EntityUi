using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.MenuItems.Dtos
{
    [Serializable]
    public class GetMenuItemListInput : PagedAndSortedResultRequestDto
    {
        public string ParentName { get; set; }
    }
}