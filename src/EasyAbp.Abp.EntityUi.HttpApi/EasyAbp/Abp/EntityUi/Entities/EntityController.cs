using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using EntityDto = EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto;

namespace EasyAbp.Abp.EntityUi.Entities
{
    [RemoteService(Name = AbpEntityUiRemoteServiceConsts.RemoteServiceName)]
    [Route("/api/abp/entity-ui/entity")]
    public class EntityController : AbpEntityUiController, IEntityAppService
    {
        private readonly IEntityAppService _service;

        public EntityController(IEntityAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<EntityDto> CreateAsync(CreateUpdateEntityDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{ModuleName}/{Name}")]
        public virtual Task<EntityDto> UpdateAsync(EntityKey id, CreateUpdateEntityDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{ModuleName}/{Name}")]
        public virtual Task DeleteAsync(EntityKey id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{ModuleName}/{Name}")]
        public virtual Task<EntityDto> GetAsync(EntityKey id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<EntityDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }
    }
}