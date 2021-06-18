using System;
using System.Reflection;
using System.Threading.Tasks;
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
            var resourceType = await GetResourceTypeAsync(module);

            return _stringLocalizerFactory.Create(resourceType);
        }

        public virtual async Task<string> GetResourceNameAsync(ModuleDto module)
        {
            var resourceType = await GetResourceTypeAsync(module);

            return resourceType.GetCustomAttribute<LocalizationResourceNameAttribute>()!.Name;
        }

        protected virtual Task<Type> GetResourceTypeAsync(ModuleDto module)
        {
            return Task.FromResult(Type.GetType($"{module.LResourceTypeName}, {module.LResourceTypeAssemblyName}"));
        }
    }
}