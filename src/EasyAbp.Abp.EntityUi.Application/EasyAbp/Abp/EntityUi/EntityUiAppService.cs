using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi
{
    public abstract class EntityUiAppService : EntityUiAppServiceBase
    {
        protected EntityUiAppService()
        {
            LocalizationResource = typeof(EntityUiResource);
            ObjectMapperContext = typeof(AbpEntityUiApplicationModule);
        }
    }
}
