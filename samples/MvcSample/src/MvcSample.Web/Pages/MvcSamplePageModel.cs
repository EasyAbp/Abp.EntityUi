using MvcSample.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace MvcSample.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class MvcSamplePageModel : AbpPageModel
    {
        protected MvcSamplePageModel()
        {
            LocalizationResourceType = typeof(MvcSampleResource);
        }
    }
}