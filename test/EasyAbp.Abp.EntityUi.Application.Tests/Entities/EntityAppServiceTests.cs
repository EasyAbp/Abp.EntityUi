using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class EntityAppServiceTests : EntityUiApplicationTestBase
    {
        private readonly IEntityAppService _entityAppService;

        public EntityAppServiceTests()
        {
            _entityAppService = GetRequiredService<IEntityAppService>();
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
