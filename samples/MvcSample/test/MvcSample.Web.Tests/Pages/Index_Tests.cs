using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace MvcSample.Pages
{
    public class Index_Tests : MvcSampleWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
