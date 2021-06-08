using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi
{
    public abstract class EntityUiAppService : ApplicationService
    {
        protected EntityUiAppService()
        {
            LocalizationResource = typeof(EntityUiResource);
            ObjectMapperContext = typeof(EntityUiApplicationModule);
        }
    }
}
