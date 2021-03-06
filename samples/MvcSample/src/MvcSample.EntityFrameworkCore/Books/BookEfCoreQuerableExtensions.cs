using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MvcSample.Books
{
    public static class BookEfCoreQueryableExtensions
    {
        public static IQueryable<Book> IncludeDetails(this IQueryable<Book> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable.Include(x => x.Tags).Include(x => x.Detail);
        }
    }
}