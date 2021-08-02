using System;
using System.Linq;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Newtonsoft.Json.Linq;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class EditModalModelModel : EditModalModelBase
    {
        protected override EntityDto EntityForAppService => CurrentEntity.GetEntity();

        protected override string IdForAppServiceUpdateMethod => Id;

        protected override string QueryPrefix => QueryPrefixEntityKey;

        public EditModalModelModel(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer,
            IServiceProvider serviceProvider,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
            : base(currentEntity, jsonSerializer, serviceProvider, integrationAppService, stringLocalizerProvider)
        {
        }

        protected override void SetBindPropertiesOnGet(object objId)
        {
            Id = JsonSerializer.Serialize(objId);
        }

        protected override void SetGetResultDtoToViewModel(object resultDto)
        {
            var json = JsonSerializer.Serialize(resultDto);
            
            ViewModel = JsonSerializer.Deserialize(CurrentEntity.GetEntity().GetAppServiceEditDtoType(), json);
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