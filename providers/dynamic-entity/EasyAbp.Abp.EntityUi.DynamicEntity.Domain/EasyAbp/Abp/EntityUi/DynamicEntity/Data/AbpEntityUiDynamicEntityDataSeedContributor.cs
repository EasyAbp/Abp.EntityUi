using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.Localization;
using EasyAbp.Abp.EntityUi.Modules;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Module = EasyAbp.Abp.EntityUi.Modules.Module;

namespace EasyAbp.Abp.EntityUi.DynamicEntity.Data
{
    public class AbpEntityUiDynamicEntityDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private const string DynamicEntityModuleName = "EasyAbp.Abp.DynamicEntity";
        
        private readonly IModuleRepository _moduleRepository;

        public AbpEntityUiDynamicEntityDataSeedContributor(
            IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }
        
        public virtual async Task SeedAsync(DataSeedContext context)
        {
            await TryCreateDynamicEntityModuleAsync();
        }

        protected virtual async Task TryCreateDynamicEntityModuleAsync()
        {
            if (await _moduleRepository.FindAsync(x => x.Name == DynamicEntityModuleName) == null)
            {
                var type = typeof(DynamicEntityResource);

                await _moduleRepository.InsertAsync(new Module(DynamicEntityModuleName, type.FullName,
                    type.Assembly.GetName().Name));
            }
        }
    }
}