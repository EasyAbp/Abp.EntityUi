using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateModalModel : CreateModalModelBase
    {
        protected override EntityDto EntityForAppService => CurrentEntity.GetEntity();

        protected override async Task CreateEntityAsync()
        {
            var entity = EntityForAppService;

            var appService = GetAppService();

            var creationDtoJson = await MapFormToDtoJsonStringAsync();

            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceCreateMethodName)!.Invoke(appService,
                    new[] {JsonSerializer.Deserialize(entity.GetAppServiceCreationDtoType(), creationDtoJson)});

            if (task != null)
            {
                await task;
            }
        }
    }
}