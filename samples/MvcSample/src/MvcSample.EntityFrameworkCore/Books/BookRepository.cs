using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcSample.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MvcSample.Books
{
    public class BookRepository : EfCoreRepository<MvcSampleDbContext, Book, Guid>, IBookRepository
    {
        public BookRepository(IDbContextProvider<MvcSampleDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Book>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).IncludeDetails();
        }

        public override async Task<IQueryable<Book>> WithDetailsAsync(params Expression<Func<Book, object>>[] propertySelectors)
        {
            return (await base.WithDetailsAsync(propertySelectors)).IncludeDetails();
        }
    }
}