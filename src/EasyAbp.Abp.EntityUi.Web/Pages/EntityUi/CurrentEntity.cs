using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CurrentEntity : ICurrentEntity, IScopedDependency
    {
        public ModuleDto Module { get; set; }
        
        public EntityDto Entity { get; set; }

        public virtual ModuleDto GetModule()
        {
            return Module;
        }

        public virtual EntityDto GetEntity()
        {
            return Entity;
        }

        public virtual void Set(ModuleDto module, EntityDto entity)
        {
            Module = module;
            Entity = entity;
        }
    }
}