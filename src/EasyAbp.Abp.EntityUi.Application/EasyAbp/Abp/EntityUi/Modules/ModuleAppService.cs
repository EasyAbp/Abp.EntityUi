using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.Abp.EntityUi.Modules
{
    public class ModuleAppService : AbstractKeyCrudAppService<Module, ModuleDto, ModuleKey, PagedAndSortedResultRequestDto, CreateUpdateModuleDto, CreateUpdateModuleDto>,
        IModuleAppService
    {

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
