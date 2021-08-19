using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos;
using EasyAbp.Abp.EntityUi.DynamicEntity;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Natasha.CSharp;
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

        public DynamicEntityEntityUiPageDataProvider(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer)
        {
            _currentEntity = currentEntity;
            _jsonSerializer = jsonSerializer;
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
            return Task.FromResult($"extraProperties.{propertyDto.Name.ToCamelCase()}");
        }

        protected virtual Task<Type> GetEditViewModelTypeAsync(EntityDto entityDto)
        {
            var nClass = NClass.RandomDomain();
            nClass
                .Namespace("EasyAbp.Abp.EntityUi.Web.Pages.EntityUi")
                .Public()
                .Name($"DynamicEntityUpdate{entityDto.Name}Dto")
                .Ctor(ctor => ctor.Public().Body(string.Empty));

            foreach (var property in entityDto.Properties.Where(x => x.ShowIn.Edit))
            {
                nClass.Property(prop => prop
                    .Type(Type.GetType(property.TypeOrEntityName))
                    .Name(property.Name)
                    .Public());
            }

            return Task.FromResult(nClass.GetType());
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

        public virtual Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto)
        {
            var nClass = NClass.RandomDomain();
            nClass
                .Namespace("EasyAbp.Abp.EntityUi.Web.Pages.EntityUi")
                .Public()
                .Name($"DynamicEntityCreate{entityDto.Name}Dto")
                .Ctor(ctor => ctor.Public().Body(string.Empty));

            foreach (var property in entityDto.Properties.Where(x => x.ShowIn.Creation))
            {
                nClass.Property(prop => prop
                    .Type(Type.GetType(property.TypeOrEntityName))
                    .Name(property.Name)
                    .Public());
            }

            return Task.FromResult(nClass.GetType());
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