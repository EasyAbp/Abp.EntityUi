using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public interface IEntityUiPageDataProvider
    {
        string EntityProviderName { get; }
        
        Task<string> MapDictionaryToCreateUpdateDtoJsonStringAsync(Dictionary<string, object> dict);
        
        Task<object> MapEntityItemDtoToViewModelAsync(object itemDto);

        Task<string> GetJsServiceCodeAsync(EntityDto entityDto);
        
        Task<string> GetJsGetListInputCodeAsync(EntityDto entityDto);
        
        Task<string> GetJsEntityListPropertyObjectCodeAsync(PropertyDto propertyDto);
    }
}