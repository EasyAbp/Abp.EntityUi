using System;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class ModuleRepository : EfCoreRepository<IEntityUiDbContext, Module>, IModuleRepository
    {
        public ModuleRepository(IDbContextProvider<IEntityUiDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}