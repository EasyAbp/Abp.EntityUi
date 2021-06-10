using System;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public interface IMenuItemAppService :
        ICrudAppService< 
            MenuItemDto, 
            MenuItemKey, 
            PagedAndSortedResultRequestDto,
            CreateUpdateMenuItemDto,
            CreateUpdateMenuItemDto>
    {

    }
}