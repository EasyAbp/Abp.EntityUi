using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
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
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}