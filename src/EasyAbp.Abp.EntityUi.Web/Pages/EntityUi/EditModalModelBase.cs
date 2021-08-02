using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;
using Volo.Abp.Json;
using Volo.Abp.ObjectMapping;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public abstract class EditModalModelBase : EntityUiModalModelBase
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public string Id { get; set; }

        protected abstract EntityDto EntityForAppService { get; }
        
        protected abstract string IdForAppServiceUpdateMethod { get; }

        protected abstract string QueryPrefix { get; }
        
        protected const string QueryPrefixEntityKey = "EntityKey_";
        protected const string QueryPrefixParentEntityKey = "ParentEntityKey_";
        
        protected readonly ICurrentEntity CurrentEntity;
        protected readonly IJsonSerializer JsonSerializer;
        protected readonly IEntityUiStringLocalizerProvider StringLocalizerProvider;
        protected IStringLocalizer StringLocalizer { get; set; }

        public EditModalModelBase(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer,
            IServiceProvider serviceProvider,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
            : base(currentEntity, jsonSerializer, serviceProvider, integrationAppService)
        {
            CurrentEntity = currentEntity;
            JsonSerializer = jsonSerializer;
            StringLocalizerProvider = stringLocalizerProvider;
        }

        public virtual async Task OnGetAsync()
        {
            await SetCurrentEntityAsync();

            StringLocalizer = await StringLocalizerProvider.GetAsync(CurrentEntity.GetModule());

            var entity = EntityForAppService;
            
            var objId = GetEntityIdForAppServiceFromQuery();

            SetBindPropertiesOnGet(objId);

            SetGetResultDtoToViewModel(await GetEntityDtoFromAppServiceAsync(entity, objId));
        }

        protected abstract void SetBindPropertiesOnGet(object objId);
        
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

        protected abstract void SetGetResultDtoToViewModel(object resultDto);

        protected virtual object GetEntityIdForAppServiceFromQuery()
        {
            var entity = EntityForAppService;
            var keys = entity.Keys.Split(',');

            var jsonId = JsonSerializer.Serialize(keys.Length > 1
                ? keys.ToDictionary(x => x.ToCamelCase(),
                    x => HttpContext.Request.Query[$"{QueryPrefix}{x.ToCamelCase()}"].FirstOrDefault())
                : HttpContext.Request.Query[$"{QueryPrefix}{keys.First().ToCamelCase()}"].FirstOrDefault());

            return ConvertIdJsonToIdObject(entity, jsonId);
        }
        
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

            var updateDto = JsonSerializer.Deserialize(entity.GetAppServiceEditDtoType(), updateDtoJObj.ToString());

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