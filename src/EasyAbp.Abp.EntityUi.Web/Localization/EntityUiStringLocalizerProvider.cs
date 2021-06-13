using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Localization
{
    public class EntityUiStringLocalizerProvider : IEntityUiStringLocalizerProvider, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityUiStringLocalizerProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public virtual Task<IStringLocalizer> GetAsync(ModuleDto module)
        {
            var resourceType = Type.GetType($"{module.LResourceTypeName}, {module.LResourceTypeAssemblyName}");
            
            var stringLocalizerType = resourceType != null
                ? typeof(IStringLocalizer<>).MakeGenericType(resourceType)
                : typeof(IStringLocalizer);
            
            return Task.FromResult((IStringLocalizer) _serviceProvider.GetRequiredService(stringLocalizerType));
        }
    }
}