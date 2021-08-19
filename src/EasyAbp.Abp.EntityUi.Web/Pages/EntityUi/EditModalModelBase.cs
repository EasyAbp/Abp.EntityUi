using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

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

            await SetGetResultDtoToViewModelAsync(await GetEntityDtoFromAppServiceAsync(entity, objId));
        }
        
        protected abstract Task SetGetResultDtoToViewModelAsync(object resultDto);

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await SetCurrentEntityAsync();

            var entity = EntityForAppService;

            var appService = GetAppService();

            var objId = ConvertIdJsonToIdObject(entity, IdForAppServiceUpdateMethod);
            
            var updateDto = await GetUpdateDtoFromFormDataAsync(entity, objId);

            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceUpdateMethodName)!.Invoke(appService,
                    new[] {objId, updateDto});

            if (task != null)
            {
                await task;
            }

            return NoContent();
        }

        protected abstract Task<object> GetUpdateDtoFromFormDataAsync(EntityDto entityDto, object objId);

        public virtual Task<string> GetModalTitleAsync()
        {
            return Task.FromResult<string>(StringLocalizer[$"Edit{Entity.Name}"]);
        }
    }
}