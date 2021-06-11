using System;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.Abp.EntityUi.Modules
{
    [RemoteService(Name = "EasyAbpAbpEntityUi")]
    [Route("/api/abp/entity-ui/module")]
    public class ModuleController : EntityUiController, IModuleAppService
    {
        private readonly IModuleAppService _service;

        public ModuleController(IModuleAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<ModuleDto> CreateAsync(CreateUpdateModuleDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{Name}")]
        public virtual Task<ModuleDto> UpdateAsync(ModuleKey id, CreateUpdateModuleDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{Name}")]
        public virtual Task DeleteAsync(ModuleKey id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{Name}")]
        public virtual Task<ModuleDto> GetAsync(ModuleKey id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<ModuleDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}