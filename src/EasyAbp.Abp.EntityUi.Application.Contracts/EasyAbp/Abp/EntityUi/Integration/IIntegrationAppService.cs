using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.Integration
{
    public interface IIntegrationAppService : IApplicationService
    {
        Task<EntityUiIntegrationDto> GetAsync();
    }
}