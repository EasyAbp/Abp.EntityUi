using MvcSample.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MvcSample.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class MvcSampleController : AbpControllerBase
    {
        protected MvcSampleController()
        {
            LocalizationResource = typeof(MvcSampleResource);
        }
    }
}