using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public static class ModuleEfCoreQueryableExtensions
    {
        public static IQueryable<Module> IncludeDetails(this IQueryable<Module> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}