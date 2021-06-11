using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.Integration
{
    public interface IIntegrationAppService : IApplicationService
    {
        Task<EntityUiIntegrationDto> GetAsync();
        
        Task<EntityUiIntegrationDto> GetModuleAsync(string moduleName);
    }
}