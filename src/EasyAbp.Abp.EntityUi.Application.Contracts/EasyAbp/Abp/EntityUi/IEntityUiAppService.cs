using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi
{
    public interface IEntityUiAppService : IApplicationService
    {
        Task<EntityUiConfigurationSet> GetConfigurationSetAsync();
    }
}