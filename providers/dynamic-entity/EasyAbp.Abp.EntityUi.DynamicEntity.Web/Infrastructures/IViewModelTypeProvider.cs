using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public interface IViewModelTypeProvider
    {
        Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto);

        Task<Type> GetEditViewModelTypeAsync(EntityDto entityDto);
    }
}