using System;
using System.Reflection;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Localization;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace EasyAbp.Abp.EntityUi.Web.Localization
{
    public class EntityUiStringLocalizerProvider : IEntityUiStringLocalizerProvider, ITransientDependency
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        public EntityUiStringLocalizerProvider(
            IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }
        
        public virtual async Task<IStringLocalizer> GetAsync(ModuleDto module)
        {
            var resourceType = await GetResourceTypeOrNullAsync(module) ?? typeof(EntityUiResource);

            return _stringLocalizerFactory.Create(resourceType);
        }

        public virtual async Task<string> GetResourceNameAsync(ModuleDto module)
        {
            var resourceType = await GetResourceTypeOrNullAsync(module) ?? typeof(EntityUiResource);

            return resourceType.GetCustomAttribute<LocalizationResourceNameAttribute>()!.Name;
        }

        public virtual Task<Type> GetResourceTypeOrNullAsync(ModuleDto module)
        {
            if (module.LResourceTypeName.IsNullOrEmpty() || module.LResourceTypeAssemblyName.IsNullOrEmpty())
            {
                return null;
            }
            
            return Task.FromResult(Type.GetType($"{module.LResourceTypeName}, {module.LResourceTypeAssemblyName}"));
        }
    }
}