using System;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public interface IModuleAppService :
        ICrudAppService< 
            ModuleDto, 
            ModuleKey, 
            PagedAndSortedResultRequestDto,
            CreateUpdateModuleDto,
            CreateUpdateModuleDto>
    {

    }
}