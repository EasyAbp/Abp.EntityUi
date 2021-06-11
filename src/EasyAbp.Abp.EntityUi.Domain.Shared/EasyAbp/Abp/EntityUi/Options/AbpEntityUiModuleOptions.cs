using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.EntityUi.Options
{
    public class AbpEntityUiModuleOptions
    {
        public Type AbpModuleType { get; }
        
        [CanBeNull]
        public string SeedJsonFilePath { get; }

        public AbpEntityUiModuleOptions(Type abpModuleType, [CanBeNull] string seedJsonFilePath = null)
        {
            AbpModuleType = abpModuleType;
            SeedJsonFilePath = seedJsonFilePath;
        }
    }
}