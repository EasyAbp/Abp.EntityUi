using JetBrains.Annotations;
using Volo.Abp.MongoDB;

namespace EasyAbp.Abp.EntityUi.MongoDB
{
    public class EntityUiMongoModelBuilderConfigurationOptions : AbpMongoModelBuilderConfigurationOptions
    {
        public EntityUiMongoModelBuilderConfigurationOptions(
            [NotNull] string collectionPrefix = "")
            : base(collectionPrefix)
        {
        }
    }
}