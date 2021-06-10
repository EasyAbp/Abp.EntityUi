using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Modules;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore.Modules
{
    public class ModuleRepositoryTests : EntityUiEntityFrameworkCoreTestBase
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleRepositoryTests()
        {
            _moduleRepository = GetRequiredService<IModuleRepository>();
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
