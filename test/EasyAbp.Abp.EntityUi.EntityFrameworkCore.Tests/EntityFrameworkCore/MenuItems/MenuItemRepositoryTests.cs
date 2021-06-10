using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.MenuItems;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore.MenuItems
{
    public class MenuItemRepositoryTests : EntityUiEntityFrameworkCoreTestBase
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemRepositoryTests()
        {
            _menuItemRepository = GetRequiredService<IMenuItemRepository>();
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
