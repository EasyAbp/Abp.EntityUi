using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public class MenuItemAppService : AbstractKeyCrudAppService<MenuItem, MenuItemDto, MenuItemKey, GetMenuItemListInput, CreateUpdateMenuItemDto, CreateUpdateMenuItemDto>,
        IMenuItemAppService
    {
        protected override string GetPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string GetListPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string CreatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string UpdatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string DeletePolicyName { get; set; } = EntityUiPermissions.Manage;

        private readonly IMenuItemRepository _repository;
        
        public MenuItemAppService(IMenuItemRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<MenuItem>> CreateFilteredQueryAsync(GetMenuItemListInput input)
        {
            return (await base.CreateFilteredQueryAsync(input)).Where(x => x.ParentName == input.ParentName);
        }

        protected override Task DeleteByIdAsync(MenuItemKey id)
        {
            // TODO: AbpHelper generated
            return _repository.DeleteAsync(e =>
                e.Name == id.Name
            );
        }

        protected override async Task<MenuItem> GetEntityByIdAsync(MenuItemKey id)
        {
            return await _repository.GetAsync(e =>
                    e.Name == id.Name
                ); 
        }

        protected override IQueryable<MenuItem> ApplyDefaultSorting(IQueryable<MenuItem> query)
        {
            // TODO: AbpHelper generated
            return query.OrderBy(e => e.Name);
        }
    }
}
