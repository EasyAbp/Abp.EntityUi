using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class ModuleAppServiceTests : EntityUiApplicationTestBase
    {
        private readonly IModuleAppService _moduleAppService;

        public ModuleAppServiceTests()
        {
            _moduleAppService = GetRequiredService<IModuleAppService>();
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
