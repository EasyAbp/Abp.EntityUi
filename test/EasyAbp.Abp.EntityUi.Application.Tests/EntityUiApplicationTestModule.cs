using EasyAbp.Abp.DynamicMenu.MenuItems;
using EasyAbp.Abp.EntityUi.Options;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    [DependsOn(
        typeof(AbpEntityUiApplicationModule),
        typeof(EntityUiDomainTestModule)
        )]
    public class EntityUiApplicationTestModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // The data seed contributor's constructor requires IMenuItemRepository, whose
            // implementation lives in the DynamicMenu driver package that this test host does not
            // reference. Provide a substitute, and clear the seed modules so the reflection/JSON
            // seeding is a no-op, letting the test application initialize for pure mapping tests.
            context.Services.AddSingleton(Substitute.For<IMenuItemRepository>());

            Configure<AbpEntityUiOptions>(options => options.Modules.Clear());
        }
    }
}
