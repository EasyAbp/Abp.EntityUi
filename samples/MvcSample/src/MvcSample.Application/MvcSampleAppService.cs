using System;
using System.Collections.Generic;
using System.Text;
using MvcSample.Localization;
using Volo.Abp.Application.Services;

namespace MvcSample
{
    /* Inherit your application services from this class.
     */
    public abstract class MvcSampleAppService : ApplicationService
    {
        protected MvcSampleAppService()
        {
            LocalizationResource = typeof(MvcSampleResource);
        }
    }
}
