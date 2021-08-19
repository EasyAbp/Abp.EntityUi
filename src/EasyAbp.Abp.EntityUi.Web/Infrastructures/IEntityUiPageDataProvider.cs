using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Newtonsoft.Json.Linq;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public interface IEntityUiPageDataProvider
    {
        string EntityProviderName { get; }
        
        Task<string> MapDictionaryToCreateUpdateDtoJsonStringAsync(Dictionary<string, object> dict);
        
        Task<string> GetJsServiceCodeAsync(EntityDto entityDto);
        
        Task<string> GetJsGetListInputCodeAsync(EntityDto entityDto);
        
        Task<string> GetJsEntityListPropertyObjectCodeAsync(PropertyDto propertyDto);

        Task MergeFormDataJsonStringIntoUpdateDtoJObjAsync(string formDataJson, JObject updateDtoJObj,
            EntityDto entityForAppService);

        Task<object> ConvertGetResultDtoToViewModelAsync(EntityDto entityDto, object resultDto);
        
        Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto);
        
        Task<object> ConvertCreationDataJsonToCreateDtoAsync(EntityDto entityDto, string dataJson);
    }
}