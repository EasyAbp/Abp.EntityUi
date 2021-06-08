using Volo.Abp.Modularity;

namespace MvcSample
{
    [DependsOn(
        typeof(MvcSampleApplicationModule),
        typeof(MvcSampleDomainTestModule)
        )]
    public class MvcSampleApplicationTestModule : AbpModule
    {

    }
}