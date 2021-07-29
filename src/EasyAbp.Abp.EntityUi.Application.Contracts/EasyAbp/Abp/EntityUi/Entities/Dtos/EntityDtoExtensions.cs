using System;

namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    public static class EntityDtoExtensions
    {
        public static string GetFullEntityName(this EntityDto entityDto)
        {
            return entityDto.Namespace.IsNullOrEmpty() ? entityDto.Name : $"{entityDto.Namespace}.{entityDto.Name}";
        }
        
        public static Type GetAppServiceInterfaceType(this EntityDto entityDto)
        {
            return Type.GetType($"{entityDto.AppServiceInterfaceName}, {entityDto.ContractsAssemblyName}");
        }
        
        public static Type GetKeyClassTypeOrNull(this EntityDto entityDto)
        {
            if (entityDto.KeyClassTypeName == null)
            {
                return null;
            }
            
            return Type.GetType($"{entityDto.KeyClassTypeName}, {entityDto.ContractsAssemblyName}");
        }
        
        public static Type GetAppServiceCreationDtoType(this EntityDto entityDto)
        {
            return Type.GetType($"{entityDto.CreationDtoTypeName}, {entityDto.ContractsAssemblyName}");
        }
        
        public static Type GetAppServiceEditDtoType(this EntityDto entityDto)
        {
            return Type.GetType($"{entityDto.EditDtoTypeName}, {entityDto.ContractsAssemblyName}");
        }
    }
}