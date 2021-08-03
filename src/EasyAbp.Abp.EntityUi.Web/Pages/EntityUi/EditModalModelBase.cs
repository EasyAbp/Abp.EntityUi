using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public abstract class EditModalModelBase : EntityUiModalModelBase
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }
        
        protected abstract string IdForAppServiceUpdateMethod { get; }

        protected IStringLocalizer StringLocalizer { get; set; }

        public virtual async Task OnGetAsync()
        {
            await SetCurrentEntityAsync();

            var currentModule = CurrentEntity.GetModule();
            
            StringLocalizer = await StringLocalizerProvider.GetAsync(currentModule);
            LocalizationResourceType = await StringLocalizerProvider.GetResourceTypeAsync(currentModule);

            var entity = EntityForAppService;
            
            var objId = GetEntityIdForAppServiceFromQuery();

            SetBindIdsOnGet(objId);

            SetGetResultDtoToViewModel(await GetEntityDtoFromAppServiceAsync(entity, objId));
        }
        
        protected abstract void SetGetResultDtoToViewModel(object resultDto);

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await SetCurrentEntityAsync();

            var entity = EntityForAppService;

            var appService = GetAppService();

            var formDataJson = MapFormToDtoJsonString();

            var objId = ConvertIdJsonToIdObject(entity, IdForAppServiceUpdateMethod);
            
            var entityDto = await GetEntityDtoFromAppServiceAsync(entity, objId);

            var updateDtoJObj = JObject.Parse(JsonSerializer.Serialize(entityDto));

            var formDataJObj = JObject.Parse(formDataJson);
            
            MergeFormDataJObjIntoUpdateDtoJObj(formDataJObj, updateDtoJObj);

            var updateDto = JsonSerializer.Deserialize(entity.GetAppServiceEditDtoType(),
                updateDtoJObj.ToString(Formatting.None));

            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceUpdateMethodName)!.Invoke(appService,
                    new[] {objId, updateDto});

            if (task != null)
            {
                await task;
            }

            return NoContent();
        }
        
        protected abstract void MergeFormDataJObjIntoUpdateDtoJObj(JObject formDataJObj, JObject updateDtoJObj);

        public virtual Task<string> GetModalTitleAsync()
        {
            return Task.FromResult<string>(StringLocalizer[$"Edit{Entity.Name}"]);
        }
    }
}