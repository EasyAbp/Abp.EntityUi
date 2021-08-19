using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Newtonsoft.Json.Linq;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public class DefaultEntityUiPageDataProvider : IEntityUiPageDataProvider, ITransientDependency
    {
        public const string DefaultEntityProviderName = AbpEntityUiConsts.DefaultEntityProviderName;
        
        public string EntityProviderName => DefaultEntityProviderName;

        private readonly IAbpLazyServiceProvider _lazyServiceProvider;
        private readonly IJsonSerializer _jsonSerializer;

        public DefaultEntityUiPageDataProvider(
            IAbpLazyServiceProvider lazyServiceProvider,
            IJsonSerializer jsonSerializer)
        {
            _lazyServiceProvider = lazyServiceProvider;
            _jsonSerializer = jsonSerializer;
        }

        public virtual Task<string> MapDictionaryToCreateUpdateDtoJsonStringAsync(Dictionary<string, object> dict)
        {
            return Task.FromResult(_jsonSerializer.Serialize(dict));
        }
        
        public virtual Task<string> GetJsServiceCodeAsync(EntityDto entityDto)
        {
            var entityNamePart = !entityDto.BelongsTo.IsNullOrEmpty()
                ? entityDto.BelongsTo.ToCamelCase()
                : entityDto.Name.ToCamelCase();

            return Task.FromResult(entityDto.Namespace.Split('.').Select(x => x.ToCamelCase()).JoinAsString(".") + '.' + entityNamePart);
        }

        public virtual Task<string> GetJsGetListInputCodeAsync(EntityDto entityDto)
        {
            return Task.FromResult("{}");
        }

        public virtual Task<string> GetJsEntityListPropertyObjectCodeAsync(PropertyDto propertyDto)
        {
            return Task.FromResult(propertyDto.Name.ToCamelCase());
        }

        public virtual Task MergeFormDataJsonStringIntoUpdateDtoJObjAsync(string formDataJson, JObject updateDtoJObj,
            EntityDto entityForAppService)
        {
            var formDataJObj =
                JObject.Parse(
                    _jsonSerializer.Serialize(
                        Activator.CreateInstance(entityForAppService.GetAppServiceEditDtoType())));

            formDataJObj.Merge(JObject.Parse(formDataJson), new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            updateDtoJObj?.Merge(formDataJObj, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });

            return Task.CompletedTask;
        }
        
        public virtual Task<object> ConvertGetResultDtoToViewModelAsync(EntityDto entityDto, object resultDto)
        {
            var json = _jsonSerializer.Serialize(resultDto);
            
            return Task.FromResult(_jsonSerializer.Deserialize(entityDto.GetAppServiceEditDtoType(), json));
        }

        public virtual Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto)
        {
            return Task.FromResult(Type.GetType($"{entityDto.CreationDtoTypeName}, {entityDto.ContractsAssemblyName}"));
        }

        public virtual Task<object> ConvertCreationDataJsonToCreateDtoAsync(EntityDto entityDto, string dataJson)
        {
            return Task.FromResult(_jsonSerializer.Deserialize(entityDto.GetAppServiceCreationDtoType(), dataJson));
        }
    }
}