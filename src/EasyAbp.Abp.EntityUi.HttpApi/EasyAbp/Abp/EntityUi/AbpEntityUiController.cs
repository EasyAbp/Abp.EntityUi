using EasyAbp.Abp.EntityUi.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.EntityUi
{
    [Area(AbpEntityUiRemoteServiceConsts.ModuleName)]
    public abstract class AbpEntityUiController : AbpControllerBase
    {
        protected AbpEntityUiController()
        {
            LocalizationResource = typeof(EntityUiResource);
        }
    }
}
