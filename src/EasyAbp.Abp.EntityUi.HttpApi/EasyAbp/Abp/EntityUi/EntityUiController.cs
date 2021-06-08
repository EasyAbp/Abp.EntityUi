using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.EntityUi
{
    public abstract class EntityUiController : AbpController
    {
        protected EntityUiController()
        {
            LocalizationResource = typeof(EntityUiResource);
        }
    }
}
