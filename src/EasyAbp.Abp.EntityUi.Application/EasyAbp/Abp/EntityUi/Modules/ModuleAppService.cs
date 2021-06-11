using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using EasyAbp.Abp.EntityUi.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class ModuleAppService : AbstractKeyCrudAppService<Module, ModuleDto, ModuleKey, PagedAndSortedResultRequestDto, CreateUpdateModuleDto, CreateUpdateModuleDto>,
        IModuleAppService
    {
        protected override string GetPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string GetListPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string CreatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string UpdatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string DeletePolicyName { get; set; } = EntityUiPermissions.Manage;

        private readonly IModuleRepository _repository;
        
        public ModuleAppService(IModuleRepository repository) : base(repository)
        {
            _repository = repository;
        }
        
        protected override Task DeleteByIdAsync(ModuleKey id)
        {
            // TODO: AbpHelper generated
            return _repository.DeleteAsync(e =>
                e.Name == id.Name
            );
        }

        protected override async Task<Module> GetEntityByIdAsync(ModuleKey id)
        {
            // TODO: AbpHelper generated
            return await AsyncExecuter.FirstOrDefaultAsync(
                _repository.Where(e =>
                    e.Name == id.Name
                )
            ); 
        }

        protected override IQueryable<Module> ApplyDefaultSorting(IQueryable<Module> query)
        {
            // TODO: AbpHelper generated
            return query.OrderBy(e => e.Name);
        }
    }
}
