using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi
{
    public abstract class EntityUiAppServiceBase : ApplicationService
    {
        protected EntityUiAppServiceBase()
        {
            LocalizationResource = typeof(EntityUiResource);
            ObjectMapperContext = typeof(AbpEntityUiApplicationModule);
        }
    }
}
