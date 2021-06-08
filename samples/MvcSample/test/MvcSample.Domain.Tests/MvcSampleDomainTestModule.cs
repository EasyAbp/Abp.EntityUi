using MvcSample.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleEntityFrameworkCoreTestModule)
        )]
    public class MvcSampleDomainTestModule : AbpModule
    {

    }
}