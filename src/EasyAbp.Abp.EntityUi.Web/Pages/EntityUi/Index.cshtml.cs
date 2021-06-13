using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class IndexModel : PageModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IIntegrationAppService _integrationAppService;
        private readonly IEntityUiStringLocalizerProvider _stringLocalizerProvider;
        private IStringLocalizer StringLocalizer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public EntityDto Entity { get; set; }

        public IndexModel(
            IAuthorizationService authorizationService,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
        {
            _authorizationService = authorizationService;
            _integrationAppService = integrationAppService;
            _stringLocalizerProvider = stringLocalizerProvider;
        }
        
        public virtual async Task OnGetAsync()
        {
            var integration = await _integrationAppService.GetModuleAsync(ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            
            var module = integration.Modules.Single(x => x.Name == ModuleName);

            StringLocalizer = await _stringLocalizerProvider.GetAsync(module);
        }
        
        public virtual async Task<bool> IsCreationPermissionGrantedAsync()
        {
            return await _authorizationService.IsGrantedAsync(Entity.CreationPermission);
        }

        public virtual Task<string> GetNewButtonTextAsync()
        {
            return Task.FromResult(StringLocalizer[$"Create{Entity.Name}"].Value);
        }

        public virtual Task<string> GetTableIdAsync()
        {
            return Task.FromResult($"{Entity.Name}Table");
        }

        public virtual Task<string> GetPageTitleAsync()
        {
            return Task.FromResult(StringLocalizer[Entity.Name].Value);
        }

        public virtual Task<string> GetBreadCrumbTextAsync()
        {
            return Task.FromResult(StringLocalizer[$"Menu:{Entity.Name}"].Value);
        }

        public virtual Task<string> GetMenuItemNameAsync()
        {
            return Task.FromResult($"{Entity.ModuleName}.{Entity.Name}");
        }

        public virtual Task<string> GetJsServiceAsync()
        {
            return Task.FromResult(Entity.Namespace.Split('.').Select(x => x.ToCamelCase()).JoinAsString(".") + '.' + EntityName.ToCamelCase());
        }

        public virtual Task<string> GetJsCreateModalSubPathAsync()
        {
            return Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/CreateModal");
        }
        
        public virtual Task<string> GetJsEditModalSubPathAsync()
        {
            return Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/EditModal");
        }

        public virtual Task<string> GetJsDeletionConfirmMessageTextAsync()
        {
            return Task.FromResult(StringLocalizer[$"{Entity.Name}DeletionConfirmationMessage"].Value);
        }

        public virtual Task<string> GetJsSuccessfullyDeletedNotificationTextAsync()
        {
            return Task.FromResult(StringLocalizer["SuccessfullyDeleted"].Value);
        }

        public virtual Task<string> GetJsDataTableDataRecordKeysCodeAsync(bool withKey = true)
        {
            return Task.FromResult(Entity.Keys.Select(key => key.ToCamelCase())
                .Select(key => withKey ? $"{key}: data.record.{key}" : $"data.record.{key}").JoinAsString(", "));
        }

        public virtual Task<string> GetJsEditRowActionItemTextAsync()
        {
            return Task.FromResult(StringLocalizer["Edit"].Value);
        }

        public virtual Task<string> GetJsDeletionRowActionItemTextAsync()
        {
            return Task.FromResult(StringLocalizer["Delete"].Value);
        }

        public virtual Task<string> GetPropertyTitleTextAsync(PropertyDto property)
        {
            return Task.FromResult($"{EntityName}{property.Name}");
        }

        public virtual Task<string> GetNewButtonIdAsync()
        {
            return Task.FromResult($"New{EntityName}Button");
        }

        public virtual async Task<string> GetJsDataTableDeletionActionInputAsync()
        {
            return Entity.Keys.Length > 1
                ? $"{{ {await GetJsDataTableDataRecordKeysCodeAsync()} }}"
                : await GetJsDataTableDataRecordKeysCodeAsync(false);
        }
    }
}