using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.EntityUi
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(EntityUiEntityFrameworkCoreTestModule)
        )]
    public class EntityUiDomainTestModule : AbpModule
    {
        
    }
}
