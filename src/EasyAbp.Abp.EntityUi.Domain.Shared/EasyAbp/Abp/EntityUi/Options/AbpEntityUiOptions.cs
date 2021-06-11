using System;
using System.Collections.Generic;

namespace EasyAbp.Abp.EntityUi.Options
{
    public class AbpEntityUiOptions
    {
        /// <summary>
        /// Mapping from module name to module's domain layer AbpModule type.
        /// Entity UI will auto discovery entities in these modules.
        /// </summary>
        public Dictionary<string, AbpEntityUiModuleOptions> Modules { get; set; }

        public AbpEntityUiOptions()
        {
            Modules = new Dictionary<string, AbpEntityUiModuleOptions>();
        }
    }
}