using EasyAbp.Abp.EntityUi.Integration.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.Integration
{
    [RemoteService(Name = AbpEntityUiRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/abp/entity-ui/integration")]
    public class IntegrationController : AbpEntityUiController, IIntegrationAppService
    {
        private readonly IIntegrationAppService _service;

        public IntegrationController(IIntegrationAppService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("")]
        public virtual Task<EntityUiIntegrationDto> GetAsync()
        {
            return _service.GetAsync();
        }

        [HttpGet]
        [Route("module/{moduleName}")]
        public virtual Task<EntityUiIntegrationDto> GetModuleAsync(string moduleName)
        {
            return _service.GetModuleAsync(moduleName);
        }
    }
}