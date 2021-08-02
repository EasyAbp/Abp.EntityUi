using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
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

            StringLocalizer = await StringLocalizerProvider.GetAsync(CurrentEntity.GetModule());

            if (IsSubEntity)
            {
                var objId = GetEntityIdForAppServiceFromQuery();

                SetBindIdsOnGet(objId);
            }

            ViewModel = Activator.CreateInstance(
                Type.GetType($"{Entity.CreationDtoTypeName}, {Entity.ContractsAssemblyName}")!);
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