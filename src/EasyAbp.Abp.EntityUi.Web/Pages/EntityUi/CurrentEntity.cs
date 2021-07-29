using System;
using System.Linq;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CurrentEntity : ICurrentEntity, IScopedDependency
    {
        private ModuleDto Module { get; set; }
        
        private EntityDto Entity { get; set; }
        
        private EntityDto ParentEntity { get; set; }

        public virtual ModuleDto GetModule()
        {
            return Module;
        }

        public virtual EntityDto GetEntity()
        {
            return Entity;
        }

        public virtual EntityDto GetParentEntityOrNull()
        {
            return ParentEntity;
        }

        public virtual void Set(EntityUiIntegrationDto integration, string moduleName, string entityName)
        {
            Module = integration.Modules.First(x => x.Name == moduleName);
            Entity = integration.Entities.First(x => x.Name == entityName);
            ParentEntity = Entity.BelongsTo.IsNullOrEmpty()
                ? null
                : integration.Entities.First(x => x.Name == Entity.BelongsTo);
        }
    }
}