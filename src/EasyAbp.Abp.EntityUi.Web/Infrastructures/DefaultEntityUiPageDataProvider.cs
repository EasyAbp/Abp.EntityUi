using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public class DefaultEntityUiPageDataProvider : IEntityUiPageDataProvider, ITransientDependency
    {
        public const string DefaultEntityProviderName = AbpEntityUiConsts.DefaultEntityProviderName;
        
        public string EntityProviderName => DefaultEntityProviderName;

        private readonly IJsonSerializer _jsonSerializer;

        public DefaultEntityUiPageDataProvider(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }

        public virtual Task<string> MapDictionaryToCreateUpdateDtoJsonStringAsync(Dictionary<string, object> dict)
        {
            return Task.FromResult(_jsonSerializer.Serialize(dict));
        }

        public virtual Task<object> MapEntityItemDtoToViewModelAsync(object itemDto)
        {
            return Task.FromResult(itemDto);
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
    }
}