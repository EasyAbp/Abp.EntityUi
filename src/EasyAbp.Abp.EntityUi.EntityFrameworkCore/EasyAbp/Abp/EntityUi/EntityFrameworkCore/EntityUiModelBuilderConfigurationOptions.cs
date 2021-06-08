using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    public class EntityUiModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public EntityUiModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}