using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Web.Infrastructures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class EditModalModel : EditModalModelBase
    {
        protected override EntityDto EntityForAppService => CurrentEntity.GetEntity();

        protected override string IdForAppServiceUpdateMethod => Id;

        protected override string QueryPrefix => QueryPrefixEntityKey;

        protected override void SetBindIdsOnGet(object objId)
        {
            Id = JsonSerializer.Serialize(objId);
        }

        protected override async Task SetGetResultDtoToViewModelAsync(object resultDto)
        {
            var dataProvider = LazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);

            ViewModel = await dataProvider.ConvertGetResultDtoToViewModelAsync(CurrentEntity.GetEntity(), resultDto);
        }

        protected override async Task<object> GetUpdateDtoFromFormDataAsync(EntityDto entityDto, object objId)
        {
            var entityObj = await GetEntityDtoFromAppServiceAsync(entityDto, objId);

            var updateDtoJObj = JObject.Parse(JsonSerializer.Serialize(entityObj));
            
            var formDataJson = await MapFormToDtoJsonStringAsync();

            var dataProvider = LazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);

            await dataProvider.MergeFormDataJsonStringIntoUpdateDtoJObjAsync(formDataJson, updateDtoJObj, entityDto);

            return JsonSerializer.Deserialize(entityDto.GetAppServiceEditDtoType(),
                updateDtoJObj.ToString(Formatting.None));
        }
    }
}