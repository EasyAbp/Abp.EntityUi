using System;
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
    }
}