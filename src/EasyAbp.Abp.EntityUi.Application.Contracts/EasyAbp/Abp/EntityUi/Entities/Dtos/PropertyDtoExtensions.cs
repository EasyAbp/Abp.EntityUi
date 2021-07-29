namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    public static class PropertyDtoExtensions
    {
        public static string GetTypeOrEntityNameWithoutNamespace(this PropertyDto propertyDto)
        {
            var str = propertyDto.TypeOrEntityName;

            return str.Substring(str.LastIndexOf('.') + 1);
        }
    }
}