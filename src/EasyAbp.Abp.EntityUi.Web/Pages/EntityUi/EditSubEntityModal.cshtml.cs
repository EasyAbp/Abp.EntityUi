using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class EditSubEntityModalModel : EditModalBase
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ParentEntityId { get; set; }
        
        protected override EntityDto EntityForAppService => CurrentEntity.GetParentEntityOrNull();

        protected override string IdForAppServiceUpdateMethod => ParentEntityId;

        protected override string QueryPrefix => QueryPrefixParentEntityKey;

        private string[] _subEntityKeys;
        private string[] SubEntityKeys
        {
            get { return _subEntityKeys ??= CurrentEntity.GetEntity().Keys.Split(','); }
        }

        public EditSubEntityModalModel(
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
            ParentEntityId = JsonSerializer.Serialize(objId);
            Id = JsonSerializer.Serialize(GetSubEntityIdFromQuery());
        }

        protected virtual object GetSubEntityIdFromQuery()
        {
            var entity = CurrentEntity.GetEntity();

            var jsonId = JsonSerializer.Serialize(entity.Keys.Split(',').ToDictionary(x => x.ToCamelCase(),
                x => HttpContext.Request.Query[$"{QueryPrefixEntityKey}{x.ToCamelCase()}"].FirstOrDefault()));
            
            return ConvertIdJsonToIdObject(entity, jsonId);
        }

        protected override void SetGetResultDtoToViewModel(object resultDto)
        {
            var parentJObj = JObject.Parse(JsonSerializer.Serialize(resultDto));

            var keyValues = Request.Query.ToDictionary(x => x.Key.RemovePreFix(QueryPrefixEntityKey),
                x => x.Value.FirstOrDefault());

            var subEntityJson = FindSubEntityJToken(parentJObj, keyValues)?.ToString();
            
            ViewModel = JsonSerializer.Deserialize(Entity.GetAppServiceEditDtoType(), subEntityJson);
        }

        protected virtual JToken FindSubEntityJToken(JObject parentJObj, Dictionary<string, string> keyValues)
        {
            var subEntityProperty = ParentEntity.Properties.First(x =>
                x.IsEntityCollection && x.TypeOrEntityName == Entity.GetFullEntityName());

            return parentJObj[subEntityProperty.Name.ToCamelCase()]?.First(x => IsSelectedSubEntity(x, keyValues));
        }

        protected virtual bool IsSelectedSubEntity(JToken jToken, Dictionary<string, string> keyValues)
        {
            return SubEntityKeys.Select(x => x.ToCamelCase()).All(key => jToken[key]?.ToString() == keyValues[key]);
        }
        
        protected override void MergeFormDataJObjIntoUpdateDtoJObj(JObject formDataJObj, JObject updateDtoJObj)
        {
            var keyValues = JsonSerializer.Deserialize<Dictionary<string, string>>(Request.Form["Id"]);
            
            var subEntityJContainer = FindSubEntityJToken(updateDtoJObj, keyValues) as JContainer;

            subEntityJContainer?.Merge(formDataJObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
        }
    }
}