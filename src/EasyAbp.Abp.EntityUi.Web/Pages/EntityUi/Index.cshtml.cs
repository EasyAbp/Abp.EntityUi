﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using JetBrains.Annotations;
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
        
        public EntityDto ParentEntity { get; set; }

        public bool IsSubEntity => !Entity.BelongsTo.IsNullOrEmpty();

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

            if (IsSubEntity)
            {
                ParentEntity = integration.Entities.Single(x => x.Name == Entity.BelongsTo);
            }
            
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

        public virtual Task<string> GetJsServiceAsync()
        {
            var entityNamePart = IsSubEntity ? Entity.BelongsTo.ToCamelCase() : Entity.Name.ToCamelCase();

            return Task.FromResult(
                Entity.Namespace.Split('.').Select(x => x.ToCamelCase()).JoinAsString(".") + '.' + entityNamePart);
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

        public virtual Task<string> GetJsDataTableDataRecordKeysCodeAsync(bool withKeys = true)
        {
            return Task.FromResult(Entity.Keys.Select(key => key.ToCamelCase())
                .Select(key => withKeys ? $"{key}: data.record.{key}" : $"data.record.{key}").JoinAsString(", "));
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

        public virtual Task<string> GetJsSubEntityCollectionPropertyNameAsync()
        {
            return Task.FromResult(IsSubEntity
                ? ParentEntity.Properties.First(x => x.IsEntityCollection && x.TypeOrEntityName == Entity.Name).Name.ToCamelCase()
                : null);
        }

        public virtual Task<string> GetJsParentEntityKeysCodeAsync()
        {
            if (!IsSubEntity)
            {
                return Task.FromResult(string.Empty);
            }

            var values = ParentEntity.Keys.Select(key => HttpContext.Request.Query[key.ToCamelCase()]).Select(x => $"'{x}'");

            return Task.FromResult(values.JoinAsString(", "));
        }

        public virtual Task<string> GetJsFindSubEntityIndexCodeAsync([NotNull] string listPropertyName,
            [NotNull] string subEntityPropertyName)
        {
            if (!IsSubEntity)
            {
                return Task.FromResult(string.Empty);
            }

            return Task.FromResult(
                $"{listPropertyName}.findIndex(x => {Entity.Keys.Select(x => x.ToCamelCase()).Select(x => $"x.{x} === {subEntityPropertyName}.{x}").JoinAsString(" && ")})");
        }

        public virtual Task<string> GetJsBuildSubEntitiesRowActionItemsAsync()
        {
            var subEntities = Entity.Properties.Where(x => x.IsEntityCollection).ToList();
            
            if (subEntities.IsNullOrEmpty())
            {
                return Task.FromResult("[]");
            }

            var keys = Entity.Keys.Select(x => x.ToCamelCase()).Select(key => $"'{key}=' + data.record.{key}").JoinAsString(" + '&' + ");

            var obj = subEntities.Select(x =>
                    $"{{ text: l('{x.TypeOrEntityName}'), action: function (data) {{ document.location.href = document.location.origin + '/EntityUi/{ModuleName}/{x.TypeOrEntityName}?' + {keys}; }} }}")
                .JoinAsString(", ");
            
            return Task.FromResult($"var subEntitiesRowActionItems = [ {obj} ]");
        }

        public virtual Task<string> GetJsDataTablePropertyNameTitleMappingAsync()
        {
            return Task.FromResult(Entity.Properties.Where(x => x.ShowIn.List)
                .Select(async x => $"{x.Name.ToCamelCase()}: `{await GetPropertyTitleTextAsync(x)}`")
                .Select(x => x.Result).JoinAsString(", "));
        }
    }
}