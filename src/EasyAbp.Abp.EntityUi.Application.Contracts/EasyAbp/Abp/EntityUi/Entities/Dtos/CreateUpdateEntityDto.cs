using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace EasyAbp.Abp.EntityUi.Entities.Dtos
{
    [Serializable]
    public class CreateUpdateEntityDto
    {
        [DisplayName("EntityModuleName")]
        public string ModuleName { get; set; }

        [DisplayName("EntityName")]
        public string Name { get; set; }

        [DisplayName("EntityNamespace")]
        public string Namespace { get; set; }

        [DisplayName("EntityBelongsTo")]
        public string BelongsTo { get; set; }

        [DisplayName("EntityKeys")]
        public string Keys { get; set; }

        [DisplayName("EntityCreationEnabled")]
        public bool CreationEnabled { get; set; }

        [DisplayName("EntityCreationPermission")]
        public string CreationPermission { get; set; }

        [DisplayName("EntityEditEnabled")]
        public bool EditEnabled { get; set; }

        [DisplayName("EntityEditPermission")]
        public string EditPermission { get; set; }

        [DisplayName("EntityDeletionEnabled")]
        public bool DeletionEnabled { get; set; }

        [DisplayName("EntityDeletionPermission")]
        public string DeletionPermission { get; set; }

        [DisplayName("EntityDetailEnabled")]
        public bool DetailEnabled { get; set; }

        [DisplayName("EntityDetailPermission")]
        public string DetailPermission { get; set; }
        
        [DisplayName("EntityContractsAssemblyName")]
        public string ContractsAssemblyName { get; set; }
        
        [DisplayName("EntityListItemDtoTypeName")]
        public string ListItemDtoTypeName { get; set; }
        
        [DisplayName("EntityDetailDtoTypeName")]
        public string DetailDtoTypeName { get; set; }
        
        [DisplayName("EntityCreationDtoTypeName")]
        public string CreationDtoTypeName { get; set; }
        
        [DisplayName("EntityEditDtoTypeName")]
        public string EditDtoTypeName { get; set; }
        
        [DisplayName("EntityGetListInputDtoTypeName")]
        public string GetListInputDtoTypeName { get; set; }
        
        [DisplayName("EntityKeyClassTypeName")]
        public string KeyClassTypeName { get; set; }
                
        [DisplayName("EntityAppServiceInterfaceName")]
        public string AppServiceInterfaceName { get; set; }
                
        [DisplayName("EntityAppServiceGetListMethodName")]
        public string AppServiceGetListMethodName { get; set; }
                
        [DisplayName("EntityAppServiceGetMethodName")]
        public string AppServiceGetMethodName { get; set; }
                
        [DisplayName("EntityAppServiceCreateMethodName")]
        public string AppServiceCreateMethodName { get; set; }
                
        [DisplayName("EntityAppServiceUpdateMethodName")]
        public string AppServiceUpdateMethodName { get; set; }
                
        [DisplayName("EntityAppServiceDeleteMethodName")]
        public string AppServiceDeleteMethodName { get; set; }

        [DisplayName("EntityProperties")]
        public List<CreateUpdatePropertyDto> Properties { get; set; }
    }
}