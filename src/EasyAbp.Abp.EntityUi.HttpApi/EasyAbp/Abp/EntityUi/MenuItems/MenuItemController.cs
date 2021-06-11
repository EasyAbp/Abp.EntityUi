using System;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    [RemoteService(Name = "EasyAbpAbpEntityUi")]
    [Route("/api/abp/entity-ui/menu-item")]
    public class MenuItemController : EntityUiController, IMenuItemAppService
    {
        private readonly IMenuItemAppService _service;

        public MenuItemController(IMenuItemAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<MenuItemDto> CreateAsync(CreateUpdateMenuItemDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{Name}")]
        public virtual Task<MenuItemDto> UpdateAsync(MenuItemKey id, CreateUpdateMenuItemDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{Name}")]
        public virtual Task DeleteAsync(MenuItemKey id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{Name}")]
        public virtual Task<MenuItemDto> GetAsync(MenuItemKey id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<MenuItemDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}