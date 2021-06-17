using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class EntityRepository : EfCoreRepository<IEntityUiDbContext, Entity>, IEntityRepository
    {
        public EntityRepository(IDbContextProvider<IEntityUiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Entity>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).IncludeDetails();
        }

        public virtual async Task<List<Entity>> GetListInModuleAsync(string moduleName, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return includeDetails
                ? await (await WithDetailsAsync()).Where(x => x.ModuleName == moduleName)
                    .ToListAsync(GetCancellationToken(cancellationToken))
                : await (await GetDbSetAsync()).Where(x => x.ModuleName == moduleName)
                    .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}