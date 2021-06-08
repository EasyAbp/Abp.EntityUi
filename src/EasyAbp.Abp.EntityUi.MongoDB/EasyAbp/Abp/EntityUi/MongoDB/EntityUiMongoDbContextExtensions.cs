using System;
using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.Abp.EntityUi.MongoDB
{
    public static class EntityUiMongoDbContextExtensions
    {
        public static void ConfigureEntityUi(
            this IMongoModelBuilder builder,
            Action<AbpMongoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EntityUiMongoModelBuilderConfigurationOptions(
                EntityUiDbProperties.DbTablePrefix
            );

            optionsAction?.Invoke(options);
        }
    }
}