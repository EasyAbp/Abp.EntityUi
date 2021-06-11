using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public interface IEntityRepository : IRepository<Entity>
    {
        Task<List<Entity>> GetListInModuleAsync(string moduleName, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken());
    }
}