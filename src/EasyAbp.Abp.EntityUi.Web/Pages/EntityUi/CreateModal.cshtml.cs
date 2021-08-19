using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Web.Infrastructures;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateModalModel : CreateModalModelBase
    {
        protected override EntityDto EntityForAppService => CurrentEntity.GetEntity();

        protected override async Task CreateEntityAsync()
        {
            var entity = EntityForAppService;

            var appService = GetAppService();

            var dataProvider = LazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);

            dynamic task = GetAppServiceType().GetInheritedMethod(entity.AppServiceCreateMethodName)!.Invoke(appService,
                new[]
                {
                    await dataProvider.ConvertCreationDataJsonToCreateDtoAsync(entity,
                        await MapFormToDtoJsonStringAsync())
                });

            if (task != null)
            {
                await task;
            }
        }
    }
}