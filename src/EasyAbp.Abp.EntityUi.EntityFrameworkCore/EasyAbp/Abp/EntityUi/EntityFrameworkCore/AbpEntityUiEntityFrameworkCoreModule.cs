using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.Entities;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpEntityUiDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class AbpEntityUiEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<EntityUiDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Entity, EntityRepository>();
                options.AddRepository<MenuItem, MenuItemRepository>();
                options.AddRepository<Module, ModuleRepository>();
            });
        }
    }
}
