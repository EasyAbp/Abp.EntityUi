using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public abstract class EntityUiModalModelBase : EntityUiPageModel
    {
        private readonly ICurrentEntity _currentEntity;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IServiceProvider _serviceProvider;
        private readonly IIntegrationAppService _integrationAppService;

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public EntityDto Entity { get; set; }
        
        public EntityDto ParentEntity { get; set; }

        public bool IsSubEntity => !Entity.BelongsTo.IsNullOrEmpty();

        public object ViewModel { get; set; }
        
        public EntityUiModalModelBase(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer,
            IServiceProvider serviceProvider,
            IIntegrationAppService integrationAppService)
        {
            _currentEntity = currentEntity;
            _jsonSerializer = jsonSerializer;
            _serviceProvider = serviceProvider;
            _integrationAppService = integrationAppService;
        }

        protected virtual Type GetAppServiceType()
        {
            return IsSubEntity ? ParentEntity.GetAppServiceInterfaceType() : Entity.GetAppServiceInterfaceType();
        }
        
        protected virtual object GetAppService()
        {
            return _serviceProvider.GetRequiredService(GetAppServiceType());
        }

        protected virtual string MapFormToDtoJsonString()
        {
            var formDict = Request.Form.ToDictionary(x => x.Key, x => x.Value.FirstOrDefault());

            var dict = new Dictionary<string, object>();

            foreach (var pair in formDict)
            {
                SetMultiLevelDictKeyValue(pair, dict);
            }

            return _jsonSerializer.Serialize(dict[nameof(ViewModel)]);
        }

        protected virtual void SetMultiLevelDictKeyValue(KeyValuePair<string, string> pair, Dictionary<string, object> dict)
        {
            var keys = pair.Key.Split('.');

            var firstKey = keys.First();

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
            var integration = await _integrationAppService.GetModuleAsync(ModuleName);

            var module = integration.Modules.Single(x => x.Name == ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            
            _currentEntity.Set(integration, module.Name, Entity.Name);

            if (IsSubEntity)
            {
                ParentEntity = integration.Entities.Single(x => x.Name == Entity.BelongsTo);
            }
        }
    }
}