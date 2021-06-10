using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public class MenuItemAppService : AbstractKeyCrudAppService<MenuItem, MenuItemDto, MenuItemKey, PagedAndSortedResultRequestDto, CreateUpdateMenuItemDto, CreateUpdateMenuItemDto>,
        IMenuItemAppService
    {

        private readonly IMenuItemRepository _repository;
        
        public MenuItemAppService(IMenuItemRepository repository) : base(repository)
        {
            _repository = repository;
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
            // TODO: AbpHelper generated
            return await AsyncExecuter.FirstOrDefaultAsync(
                _repository.Where(e =>
                    e.Name == id.Name
                )
            ); 
        }

        protected override IQueryable<MenuItem> ApplyDefaultSorting(IQueryable<MenuItem> query)
        {
            // TODO: AbpHelper generated
            return query.OrderBy(e => e.Name);
        }
    }
}
