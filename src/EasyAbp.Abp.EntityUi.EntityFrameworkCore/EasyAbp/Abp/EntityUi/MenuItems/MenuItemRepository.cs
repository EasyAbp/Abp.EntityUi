using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public class MenuItemRepository : EfCoreRepository<IEntityUiDbContext, MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(IDbContextProvider<IEntityUiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<MenuItem>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}