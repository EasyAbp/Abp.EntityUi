using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using EasyAbp.Abp.EntityUi.Web.Infrastructures;
using EasyAbp.Abp.EntityUi.Web.Localization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class IndexModel : PageModel
    {
        private readonly IAbpLazyServiceProvider _lazyServiceProvider;
        private readonly IAuthorizationService _authorizationService;
        private readonly IIntegrationAppService _integrationAppService;
        private readonly IEntityUiStringLocalizerProvider _stringLocalizerProvider;
        
        private IEntityUiPageDataProvider PageDataProvider;
        private IStringLocalizer StringLocalizer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public ModuleDto Module { get; set; }
        
        public EntityDto Entity { get; set; }
        
        public EntityDto ParentEntity { get; set; }
        
        public string[] EntityKeys { get; set; }
        
        public string[] ParentEntityKeys { get; set; }

        public bool IsSubEntity => !Entity.BelongsTo.IsNullOrEmpty();
        
        public IndexModel(
            IAbpLazyServiceProvider lazyServiceProvider,
            IAuthorizationService authorizationService,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
        {
            _lazyServiceProvider = lazyServiceProvider;
            _authorizationService = authorizationService;
            _integrationAppService = integrationAppService;
            _stringLocalizerProvider = stringLocalizerProvider;
        }
        
        public virtual async Task OnGetAsync()
        {
            var integration = await _integrationAppService.GetModuleAsync(ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            EntityKeys = Entity.Keys.Split(',');

            if (IsSubEntity)
            {
                ParentEntity = integration.Entities.Single(x => x.Name == Entity.BelongsTo);
                ParentEntityKeys = ParentEntity.Keys.Split(',');
            }
            
            Module = integration.Modules.Single(x => x.Name == ModuleName);

            StringLocalizer = await _stringLocalizerProvider.GetAsync(Module);
            PageDataProvider = _lazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);
        }
        
        public virtual async Task<bool> IsCreationPermissionGrantedOrNullAsync()
        {
            if (Entity.CreationPermission.IsNullOrWhiteSpace())
            {
                return true;
            }
            
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
            var localizationItemName = IsSubEntity ? $"Menu:{Entity.BelongsTo}" : $"Menu:{Entity.Name}";
            
            return Task.FromResult(StringLocalizer[localizationItemName].Value);
        }

        public virtual Task<string> GetMenuItemNameAsync()
        {
            var menuItemName = IsSubEntity
                ? $"{Entity.ModuleName}.{Entity.BelongsTo}"
                : $"{Entity.ModuleName}.{Entity.Name}";

            return Task.FromResult(menuItemName);
        }

        public virtual async Task<string> GetJsServiceAsync()
        {
            return await PageDataProvider.GetJsServiceCodeAsync(Entity);
        }

        public virtual Task<string> GetJsGetListMethodNameAsync()
        {
            return Task.FromResult(Entity.AppServiceGetListMethodName.RemovePostFix("Async").ToCamelCase());
        }

        public virtual async Task<string> GetJsGetListInputCodeAsync()
        {
            return await PageDataProvider.GetJsGetListInputCodeAsync(Entity);
        }

        public virtual Task<string> GetJsCreateModalSubPathAsync()
        {
            return IsSubEntity
                ? Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/CreateSubEntityModal")
                : Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/CreateModal");
        }
        
        public virtual Task<string> GetJsEditModalSubPathAsync()
        {
            return IsSubEntity
                ? Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/EditSubEntityModal")
                : Task.FromResult($"EntityUi/{ModuleName}/{EntityName}/EditModal");
        }

        public virtual Task<string> GetJsDeletionConfirmMessageTextAsync()
        {
            return Task.FromResult(StringLocalizer[$"{Entity.Name}DeletionConfirmationMessage"].Value);
        }

        public virtual Task<string> GetJsSuccessfullyDeletedNotificationTextAsync()
        {
            return Task.FromResult(StringLocalizer["SuccessfullyDeleted"].Value);
        }

        public virtual Task<string> GetJsDataTableDataRecordKeysCodeAsync(bool withKeys = true)
        {
            var entityKeys = EntityKeys.Select(key => key.ToCamelCase())
                .Select(key => withKeys ? $"EntityKey_{key}: data.record.{key}" : $"data.record.{key}");

            var parentEntityKeys = ParentEntityKeys.Select(key => $"ParentEntityKey_{key.ToCamelCase()}")
                .Select(keyWithPrefix =>
                    withKeys
                        ? $"{keyWithPrefix}: '{HttpContext.Request.Query[keyWithPrefix]}'"
                        : $"'{HttpContext.Request.Query[keyWithPrefix]}'");
            
            return Task.FromResult(entityKeys.Concat(parentEntityKeys).JoinAsString(", "));
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
            return EntityKeys.Length > 1
                ? $"{{ {await GetJsDataTableDataRecordKeysCodeAsync()} }}"
                : await GetJsDataTableDataRecordKeysCodeAsync(false);
        }

        public virtual Task<string> GetJsSubEntityCollectionPropertyNameAsync()
        {
            return Task.FromResult(IsSubEntity
                ? ParentEntity.Properties
                    .First(x => x.IsEntityCollection && x.TypeOrEntityName == Entity.GetFullEntityName()).Name
                    .ToCamelCase()
                : null);
        }

        public virtual Task<string> GetJsParentEntityKeysCodeAsync(bool keyWithPrefix, bool autoRemoveKey = true)
        {
            if (!IsSubEntity)
            {
                return Task.FromResult(string.Empty);
            }

            if (autoRemoveKey && ParentEntityKeys.Length == 1)
            {
                return Task.FromResult(
                    $"\"{HttpContext.Request.Query[EntityUiModalModelBase.QueryPrefixParentEntityKey + ParentEntityKeys.First().ToCamelCase()]}\"");
            }

            var keyPrefix = keyWithPrefix ? EntityUiModalModelBase.QueryPrefixParentEntityKey : "";

            var values = ParentEntityKeys.Select(x => x.ToCamelCase()).Select(key =>
                $"{keyPrefix}{key}: \"{HttpContext.Request.Query[EntityUiModalModelBase.QueryPrefixParentEntityKey + key]}\"");

            return Task.FromResult($"{{ {values.JoinAsString(", ")} }}");
        }

        public virtual Task<string> GetJsFindSubEntityIndexCodeAsync([NotNull] string listPropertyName,
            [NotNull] string subEntityPropertyName)
        {
            if (!IsSubEntity)
            {
                return Task.FromResult(string.Empty);
            }

            return Task.FromResult(
                $"{listPropertyName}.findIndex(x => {EntityKeys.Select(x => x.ToCamelCase()).Select(x => $"x.{x} === {subEntityPropertyName}.{x}").JoinAsString(" && ")})");
        }

        public virtual Task<string> GetJsBuildSubEntitiesRowActionItemsAsync()
        {
            var subEntities = Entity.Properties.Where(x => x.IsEntityCollection).ToList();
            
            if (subEntities.IsNullOrEmpty())
            {
                return Task.FromResult("var subEntitiesRowActionItems = []");
            }

            const string prefix = EntityUiModalModelBase.QueryPrefixParentEntityKey;

            var keys = EntityKeys.Select(x => x.ToCamelCase()).Select(key => $"'{prefix}{key}=' + data.record.{key}").JoinAsString(" + '&' + ");

            var obj = subEntities.Select(x =>
                    $"{{ text: l('{Entity.Name}{x.Name}'), action: function (data) {{ document.location.href = document.location.origin + '/EntityUi/{ModuleName}/{x.GetTypeOrEntityNameWithoutNamespace()}?' + {keys}; }} }}")
                .JoinAsString(", ");
            
            return Task.FromResult($"var subEntitiesRowActionItems = [ {obj} ]");
        }

        public virtual Task<string> GetJsDataTablePropertyNameTitleMappingAsync()
        {
            var properties = Entity.Properties;

            properties.Reverse();
            
            return Task.FromResult(properties.Where(x => x.ShowIn.List)
                .Select(async x => $"\"{await PageDataProvider.GetJsEntityListPropertyObjectCodeAsync(x)}\": `{await GetPropertyTitleTextAsync(x)}`")
                .Select(x => x.Result).JoinAsString(", "));
        }

        public async Task<string> GetJsLocalizationResourceNameAsync()
        {
            return await _stringLocalizerProvider.GetResourceNameAsync(Module);
        }
    }
}