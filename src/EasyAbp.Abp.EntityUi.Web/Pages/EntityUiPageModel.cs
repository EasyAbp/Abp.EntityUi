using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.Abp.EntityUi.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class EntityUiPageModel : AbpPageModel
    {
        protected EntityUiPageModel()
        {
            LocalizationResourceType = typeof(EntityUiResource);
            ObjectMapperContext = typeof(AbpEntityUiWebModule);
        }
    }
}