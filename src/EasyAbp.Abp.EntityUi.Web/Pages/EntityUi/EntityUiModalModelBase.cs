using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Infrastructures;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public abstract class EntityUiModalModelBase : EntityUiPageModel
    {
        public const string QueryPrefixEntityKey = "EntityKey_";
        public const string QueryPrefixParentEntityKey = "ParentEntityKey_";
        
        protected ICurrentEntity CurrentEntity => LazyServiceProvider.LazyGetRequiredService<ICurrentEntity>();
        protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();
        protected IIntegrationAppService IntegrationAppService => LazyServiceProvider.LazyGetRequiredService<IIntegrationAppService>();
        protected IEntityUiStringLocalizerProvider StringLocalizerProvider => LazyServiceProvider.LazyGetRequiredService<IEntityUiStringLocalizerProvider>();

        protected abstract EntityDto EntityForAppService { get; }
        protected abstract string QueryPrefix { get; }

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public EntityDto Entity { get; set; }
        
        public EntityDto ParentEntity { get; set; }

        public bool IsSubEntity => !Entity.BelongsTo.IsNullOrEmpty();

        public object ViewModel { get; set; }

        protected virtual void SetBindIdsOnGet(object objId)
        {
        }
        
        protected virtual object GetEntityIdForAppServiceFromQuery()
        {
            var entity = EntityForAppService;
            var keys = entity.Keys.Split(',');

            var jsonId = JsonSerializer.Serialize(entity.GetKeyClassTypeOrNull() != null
                ? keys.ToDictionary(x => x.ToCamelCase(),
                    x => HttpContext.Request.Query[$"{QueryPrefix}{x.ToCamelCase()}"].FirstOrDefault())
                : HttpContext.Request.Query[$"{QueryPrefix}{keys.First().ToCamelCase()}"].FirstOrDefault());

            return ConvertIdJsonToIdObject(entity, jsonId);
        }

        protected virtual Type GetAppServiceType()
        {
            return IsSubEntity ? ParentEntity.GetAppServiceInterfaceType() : Entity.GetAppServiceInterfaceType();
        }
        
        protected virtual object GetAppService()
        {
            return LazyServiceProvider.LazyGetRequiredService(GetAppServiceType());
        }

        protected virtual Task<string> MapFormToDtoJsonStringAsync()
        {
            var formDict = Request.Form.Where(x => x.Key.StartsWith($"{nameof(ViewModel)}."))
                .ToDictionary(x => x.Key.RemovePreFix($"{nameof(ViewModel)}."), x => x.Value.FirstOrDefault());

            var dict = new Dictionary<string, object>();

            foreach (var pair in formDict)
            {
                SetMultiLevelDictKeyValue(pair, dict);
            }

            var dataProvider = LazyServiceProvider.GetEntityUiPageDataProviderOrDefault(Entity.ProviderName);
            
            return dataProvider.MapDictionaryToCreateUpdateDtoJsonStringAsync(dict);
        }

        protected virtual void SetMultiLevelDictKeyValue(KeyValuePair<string, string> pair, Dictionary<string, object> dict)
        {
            var keys = pair.Key.Split('.');

            var firstKey = keys.First().ToCamelCase();

            if (keys.Length == 1)
            {
                dict[firstKey] = pair.Value;
            }
            else
            {
                if (!dict.ContainsKey(firstKey))
                {
                    dict[firstKey] = new Dictionary<string, object>();
                }

                SetMultiLevelDictKeyValue(new KeyValuePair<string, string>(pair.Key.Split(new[] {'.'}, 2)[1], pair.Value),
                    (Dictionary<string, object>) dict[firstKey]);
            }
        }

        protected virtual async Task SetCurrentEntityAsync()
        {
            var integration = await IntegrationAppService.GetModuleAsync(ModuleName);

            var module = integration.Modules.Single(x => x.Name == ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            
            CurrentEntity.Set(integration, module.Name, Entity.Name);

            if (IsSubEntity)
            {
                ParentEntity = integration.Entities.Single(x => x.Name == Entity.BelongsTo);
            }
        }
        
        protected virtual object ConvertIdJsonToIdObject(EntityDto entityDto, string jsonId)
        {
            var keyType = entityDto.GetKeyClassTypeOrNull();

            if (keyType != null)
            {
                return JsonSerializer.Deserialize(keyType, jsonId);
            }

            var keys = entityDto.Keys.Split(',');

            if (keys.Length == 1)
            {
                keyType = Type.GetType(entityDto.Properties.Single(x => x.Name == entityDto.Keys).TypeOrEntityName);

                var jsonIdString = JsonSerializer.Deserialize<string>(jsonId);
                
                return keyType == typeof(Guid) ? Guid.Parse(jsonIdString) : Convert.ChangeType(jsonIdString, keyType!);
            }

            return JsonSerializer.Deserialize<object>(jsonId);
        }
        
        protected virtual async Task<object> GetEntityDtoFromAppServiceAsync(EntityDto entity, object id)
        {
            var appService = GetAppService();

            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceGetMethodName)!.Invoke(appService,
                    new[] {id});

            if (task == null)
            {
                return null;
            }

            return await task;
        }
    }
}