using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public interface ICurrentEntity
    {
        ModuleDto GetModule();
        
        EntityDto GetEntity();
        
        void Set(ModuleDto module, EntityDto entity);
    }
}