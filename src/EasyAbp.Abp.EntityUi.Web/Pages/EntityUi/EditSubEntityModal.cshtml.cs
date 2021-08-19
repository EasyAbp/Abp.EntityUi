using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class EditSubEntityModalModel : EditModalModelBase
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

        protected override void SetBindIdsOnGet(object objId)
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

        protected override Task SetGetResultDtoToViewModelAsync(object resultDto)
        {
            var parentJObj = JObject.Parse(JsonSerializer.Serialize(resultDto));

            var keyValues = Request.Query.ToDictionary(x => x.Key.RemovePreFix(QueryPrefixEntityKey),
                x => x.Value.FirstOrDefault());

            var subEntityJson = FindSubEntityJToken(parentJObj, keyValues)?.ToString();
            
            ViewModel = JsonSerializer.Deserialize(Entity.GetAppServiceEditDtoType(), subEntityJson);
            
            return Task.CompletedTask;
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

        protected override async Task<object> GetUpdateDtoFromFormDataAsync(EntityDto entityDto, object objId)
        {
            var entityObj = await GetEntityDtoFromAppServiceAsync(entityDto, objId);

            var updateDtoJObj = JObject.Parse(JsonSerializer.Serialize(entityObj));
            
            var formDataJson = await MapFormToDtoJsonStringAsync();

            var formDataJObj =
                JObject.Parse(
                    JsonSerializer.Serialize(Activator.CreateInstance(Entity.GetAppServiceEditDtoType())));
            
            formDataJObj.Merge(JObject.Parse(formDataJson), new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            var keyValues = JsonSerializer.Deserialize<Dictionary<string, string>>(Request.Form["Id"]);
            
            var subEntityJContainer = FindSubEntityJToken(updateDtoJObj, keyValues) as JContainer;

            subEntityJContainer?.Merge(formDataJObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            return JsonSerializer.Deserialize(entityDto.GetAppServiceEditDtoType(),
                updateDtoJObj.ToString(Formatting.None));
        }
    }
}