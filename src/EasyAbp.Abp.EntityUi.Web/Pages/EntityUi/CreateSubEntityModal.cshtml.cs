using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateSubEntityModalModel : CreateModalModelBase
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string ParentEntityId { get; set; }
        
        protected override EntityDto EntityForAppService => CurrentEntity.GetParentEntityOrNull();

        protected override async Task CreateEntityAsync()
        {
            var entity = EntityForAppService;

            var appService = GetAppService();

            var formDataJson = MapFormToDtoJsonString();

            var objId = ConvertIdJsonToIdObject(entity, ParentEntityId);

            var updateDtoJson = await CreateUpdateDtoJsonAsync(formDataJson);

            var updateDto = JsonSerializer.Deserialize(entity.GetAppServiceEditDtoType(), updateDtoJson);

            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceUpdateMethodName)!.Invoke(appService,
                    new[] {objId, updateDto});

            if (task != null)
            {
                await task;
            }
        }
        
        protected override void SetBindIdsOnGet(object objId)
        {
            ParentEntityId = JsonSerializer.Serialize(objId);
        }
        
        protected virtual async Task<string> CreateUpdateDtoJsonAsync(string formDataJson)
        {
            var entity = EntityForAppService;
            
            var objId = ConvertIdJsonToIdObject(entity, ParentEntityId);
            
            var entityDto = await GetEntityDtoFromAppServiceAsync(entity, objId);

            var updateDtoJObj = JObject.Parse(JsonSerializer.Serialize(entityDto));

            var subEntityUpdateDtoJObj =
                JObject.Parse(
                    JsonSerializer.Serialize(
                        Activator.CreateInstance(CurrentEntity.GetEntity().GetAppServiceEditDtoType())));

            var formDataJObj = JObject.Parse(formDataJson);
            
            subEntityUpdateDtoJObj.Merge(formDataJObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            
            var subEntityProperty = ParentEntity.Properties.First(x =>
                x.IsEntityCollection && x.TypeOrEntityName == Entity.GetFullEntityName());

            var subEntityPropertyJContainer = updateDtoJObj[subEntityProperty.Name.ToCamelCase()] as JContainer;
            
            subEntityPropertyJContainer?.Add(subEntityUpdateDtoJObj);

            return updateDtoJObj.ToString(Formatting.None);
        }
    }
}