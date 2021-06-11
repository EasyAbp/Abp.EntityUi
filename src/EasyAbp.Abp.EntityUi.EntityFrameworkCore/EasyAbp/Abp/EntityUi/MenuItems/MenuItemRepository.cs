using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public virtual async Task<List<MenuItem>> GetListAsync(string parentName, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return includeDetails
                ? await (await WithDetailsAsync()).Where(x => x.ParentName == parentName)
                    .ToListAsync(GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).Where(x => x.ParentName == parentName)
                    .ToListAsync(GetCancellationToken(cancellationToken));
        }

        [Obsolete]
        public override Task<List<MenuItem>> GetListAsync(bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }

        [Obsolete]
        public override Task<List<MenuItem>> GetListAsync(Expression<Func<MenuItem, bool>> predicate, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }

        [Obsolete]
        public override Task<List<MenuItem>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotSupportedException();
        }
    }
}