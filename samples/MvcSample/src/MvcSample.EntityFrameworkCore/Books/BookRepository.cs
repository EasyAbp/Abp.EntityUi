using System;
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
    }
}