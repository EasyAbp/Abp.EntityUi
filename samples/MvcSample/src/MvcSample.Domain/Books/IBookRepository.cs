using System;
using Volo.Abp.Domain.Repositories;

namespace MvcSample.Books
{
    public interface IBookRepository : IRepository<Book, Guid>
    {
    }
}