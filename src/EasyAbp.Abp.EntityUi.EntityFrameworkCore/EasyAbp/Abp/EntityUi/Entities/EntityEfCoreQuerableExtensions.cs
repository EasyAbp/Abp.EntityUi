using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public static class EntityEfCoreQueryableExtensions
    {
        public static IQueryable<Entity> IncludeDetails(this IQueryable<Entity> queryable, bool include = true)
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