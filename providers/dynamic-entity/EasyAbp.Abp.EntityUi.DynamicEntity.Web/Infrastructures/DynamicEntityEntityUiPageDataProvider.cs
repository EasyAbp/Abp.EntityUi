using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicEntity.DynamicEntities.Dtos;
using EasyAbp.Abp.EntityUi.DynamicEntity;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
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
            
            var dto = new CreateUpdateDynamicEntityDto
            {
                ModelDefinitionId = entity.GetProperty<Guid>(AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName)
            };

            foreach (var (key, value) in dict)
            {
                dto.SetProperty(key, value);
            }
            
            return Task.FromResult(_jsonSerializer.Serialize(dict));
        }

        public virtual Task<object> MapEntityItemDtoToViewModelAsync(object itemDto)
        {
            return Task.FromResult(itemDto);
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
    }
}