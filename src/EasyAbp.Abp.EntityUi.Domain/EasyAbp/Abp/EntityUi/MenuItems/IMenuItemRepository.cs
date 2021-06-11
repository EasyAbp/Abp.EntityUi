using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        Task<List<MenuItem>> GetListAsync([CanBeNull] string parentName, bool includeDetails = false,
            CancellationToken cancellationToken = new CancellationToken());

        Task<List<MenuItem>> GetListInModuleAsync(string moduleName, [CanBeNull] string parentName,
            bool includeDetails = false, CancellationToken cancellationToken = new CancellationToken());
    }
}