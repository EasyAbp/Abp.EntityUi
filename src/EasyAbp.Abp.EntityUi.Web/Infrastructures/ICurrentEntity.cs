using System.Diagnostics.CodeAnalysis;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public interface ICurrentEntity
    {
        ModuleDto GetModule();
        
        EntityDto GetEntity();
        
        EntityDto GetParentEntityOrNull();
        
        void Set(EntityUiIntegrationDto integration, [NotNull] string moduleName, [NotNull] string entityName);
    }
}