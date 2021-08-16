using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public static class AbpLazyServiceProviderExtensions
    {
        public static IEntityUiPageDataProvider GetEntityUiPageDataProviderOrDefault(
            this IAbpLazyServiceProvider lazyServiceProvider, [NotNull] string entityProviderName)
        {
            var provider = lazyServiceProvider.LazyGetService<IEnumerable<IEntityUiPageDataProvider>>()
                .FirstOrDefault(x => x.EntityProviderName == entityProviderName);

            return provider ?? lazyServiceProvider.LazyGetRequiredService<DefaultEntityUiPageDataProvider>();
        }
    }
}