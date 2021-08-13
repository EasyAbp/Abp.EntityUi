using System;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
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

        protected override void SetGetResultDtoToViewModel(object resultDto)
        {
            var json = JsonSerializer.Serialize(resultDto);
            
            ViewModel = JsonSerializer.Deserialize(CurrentEntity.GetEntity().GetAppServiceEditDtoType(), json);
        }

        protected override JObject GetFormDataJObj()
        {
            return JObject.Parse(
                JsonSerializer.Serialize(Activator.CreateInstance(EntityForAppService.GetAppServiceEditDtoType())));
        }

        protected override void MergeFormDataJObjIntoUpdateDtoJObj(JObject formDataJObj, JObject updateDtoJObj)
        {
            updateDtoJObj.Merge(formDataJObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
        }
    }
}