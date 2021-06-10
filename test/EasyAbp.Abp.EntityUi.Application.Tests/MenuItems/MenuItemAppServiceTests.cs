using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.Abp.EntityUi.MenuItems
{
    public class MenuItemAppServiceTests : EntityUiApplicationTestBase
    {
        private readonly IMenuItemAppService _menuItemAppService;

        public MenuItemAppServiceTests()
        {
            _menuItemAppService = GetRequiredService<IMenuItemAppService>();
        }

        /*
        [Fact]
        public async Task Test1()
        {
            // Arrange

            // Act

            // Assert
        }
        */
    }
}
