using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Localization;
using EasyAbp.Abp.EntityUi.Web.Infrastructures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public abstract class CreateModalModelBase : EntityUiModalModelBase
    {
        protected override string QueryPrefix => QueryPrefixParentEntityKey;

        private IStringLocalizer StringLocalizer { get; set; }

        public virtual async Task OnGetAsync()
        {
            await SetCurrentEntityAsync();

            var currentModule = CurrentEntity.GetModule();
            
            StringLocalizer = await StringLocalizerProvider.GetAsync(currentModule);
            LocalizationResourceType = await StringLocalizerProvider.GetResourceTypeOrNullAsync(currentModule) ??
                                       typeof(EntityUiResource);

            if (IsSubEntity)
            {
                var objId = GetEntityIdForAppServiceFromQuery();

                SetBindIdsOnGet(objId);
            }

            var dataProvider = LazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);

            ViewModel = Activator.CreateInstance(
                await dataProvider.GetCreationViewModelTypeAsync(CurrentEntity.GetEntity()));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await SetCurrentEntityAsync();
            
            await CreateEntityAsync();

            return NoContent();
        }

        protected abstract Task CreateEntityAsync();

        public virtual Task<string> GetModalTitleAsync()
        {
            return Task.FromResult<string>(StringLocalizer[$"Create{Entity.Name}"]);
        }
    }
}