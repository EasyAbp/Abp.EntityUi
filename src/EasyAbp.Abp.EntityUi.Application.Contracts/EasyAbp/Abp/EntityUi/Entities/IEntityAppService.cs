using System;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EntityDto = EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public interface IEntityAppService :
        ICrudAppService< 
            EntityDto, 
            EntityKey, 
            PagedAndSortedResultRequestDto,
            CreateUpdateEntityDto,
            CreateUpdateEntityDto>
    {

    }
}