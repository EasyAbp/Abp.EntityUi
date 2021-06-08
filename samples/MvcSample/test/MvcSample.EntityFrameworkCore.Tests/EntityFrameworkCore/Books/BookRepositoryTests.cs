using System;
using System.Threading.Tasks;
using MvcSample.Books;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace MvcSample.EntityFrameworkCore.Books
{
    public class BookRepositoryTests : MvcSampleEntityFrameworkCoreTestBase
    {
        private readonly IBookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = GetRequiredService<IBookRepository>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange

                // Act

                //Assert
            });
        }
        */
    }
}
