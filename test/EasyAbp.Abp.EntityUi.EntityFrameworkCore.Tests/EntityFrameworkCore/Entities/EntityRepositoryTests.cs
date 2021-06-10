using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore.Entities
{
    public class EntityRepositoryTests : EntityUiEntityFrameworkCoreTestBase
    {
        private readonly IEntityRepository _entityRepository;

        public EntityRepositoryTests()
        {
            _entityRepository = GetRequiredService<IEntityRepository>();
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
