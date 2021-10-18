using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Microsoft.Extensions.Localization;

namespace EasyAbp.Abp.EntityUi.Web.Localization
{
    public interface IEntityUiStringLocalizerProvider
    {
        Task<IStringLocalizer> GetAsync(ModuleDto module);
        
        Task<string> GetResourceNameAsync(ModuleDto module);

        Task<Type> GetResourceTypeOrNullAsync(ModuleDto module);
    }
}