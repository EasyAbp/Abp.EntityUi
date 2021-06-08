using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace MvcSample.Web
{
    [Dependency(ReplaceServices = true)]
    public class MvcSampleBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "MvcSample";
    }
}
