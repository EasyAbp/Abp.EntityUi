using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos;
using EasyAbp.Abp.EntityUi.DynamicEntity;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Newtonsoft.Json.Linq;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public class DynamicEntityEntityUiPageDataProvider : IEntityUiPageDataProvider, ITransientDependency
    {
        public const string DynamicEntityEntityProviderName = AbpEntityUiDynamicEntityConsts.DynamicEntityEntityProviderName;
        
        public string EntityProviderName => DynamicEntityEntityProviderName;

        private readonly ICurrentEntity _currentEntity;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IViewModelTypeProvider _viewModelTypeProvider;

        public DynamicEntityEntityUiPageDataProvider(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer,
            IViewModelTypeProvider viewModelTypeProvider)
        {
            _currentEntity = currentEntity;
            _jsonSerializer = jsonSerializer;
            _viewModelTypeProvider = viewModelTypeProvider;
        }

        public virtual Task<string> MapDictionaryToCreateUpdateDtoJsonStringAsync(Dictionary<string, object> dict)
        {
            var entity = _currentEntity.GetEntity();
            
            var dto = new CreateDynamicEntityDto
            {
                ModelDefinitionId = entity.GetProperty<Guid>(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName)
            };

            foreach (var (key, value) in dict)
            {
                dto.SetProperty(key, value);
            }
            
            return Task.FromResult(_jsonSerializer.Serialize(dict));
        }

        public virtual Task<string> GetJsServiceCodeAsync(EntityDto entityDto)
        {
            return Task.FromResult("easyAbp.abp.dynamicEntity.dynamicEntities.dynamicEntity");
        }

        public virtual Task<string> GetJsGetListInputCodeAsync(EntityDto entityDto)
        {
            var modelDefinitionId =
                entityDto.GetProperty<Guid>(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName);

            return Task.FromResult($"{{ modelDefinitionId: \"{modelDefinitionId}\" }}");
        }

        public virtual Task<string> GetJsEntityListPropertyObjectCodeAsync(PropertyDto propertyDto)
        {
            return Task.FromResult($"extraProperties.{propertyDto.Name}");
        }
        
        public virtual async Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto)
        {
            return await _viewModelTypeProvider.GetCreationViewModelTypeAsync(entityDto);
        }

        protected virtual async Task<Type> GetEditViewModelTypeAsync(EntityDto entityDto)
        {
            return await _viewModelTypeProvider.GetEditViewModelTypeAsync(entityDto);
        }

        public Task MergeFormDataJsonStringIntoUpdateDtoJObjAsync(string formDataJson, JObject updateDtoJObj,
            EntityDto entityForAppService)
        {
            var formDataJObj = JObject.Parse(formDataJson);

            updateDtoJObj?.Merge(new JObject {{"extraProperties", formDataJObj}}, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace
            });

            return Task.CompletedTask;
        }

        public virtual async Task<object> ConvertGetResultDtoToViewModelAsync(EntityDto entityDto, object resultDto)
        {
            var json = _jsonSerializer.Serialize(resultDto);

            return _jsonSerializer.Deserialize(await GetEditViewModelTypeAsync(entityDto),
                JObject.Parse(json)["extraProperties"]!.ToString());
        }

        public virtual Task<object> ConvertCreationDataJsonToCreateDtoAsync(EntityDto entityDto, string dataJson)
        {
            var modelDefinitionId =
                entityDto.GetProperty<Guid>(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName);

            var creationJObj = new JObject(
                new JProperty("modelDefinitionId", modelDefinitionId),
                new JProperty("extraProperties", JObject.Parse(dataJson))
            );

            return Task.FromResult(_jsonSerializer.Deserialize(entityDto.GetAppServiceCreationDtoType(),
                creationJObj.ToString()));
        }
    }
}