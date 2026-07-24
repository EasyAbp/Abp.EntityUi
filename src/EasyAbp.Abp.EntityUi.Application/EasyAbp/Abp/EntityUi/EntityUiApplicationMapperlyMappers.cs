using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.Abp.EntityUi
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class EntityToEntityDtoMapper : MapperBase<Entity, EntityDto>
    {
        public override partial EntityDto Map(Entity source);

        public override partial void Map(Entity source, EntityDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PropertyToPropertyDtoMapper : MapperBase<Property, PropertyDto>
    {
        public override partial PropertyDto Map(Property source);

        public override partial void Map(Property source, PropertyDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class PropertyShowInValueObjectToPropertyShowInDtoMapper : MapperBase<PropertyShowInValueObject, PropertyShowInDto>
    {
        public override partial PropertyShowInDto Map(PropertyShowInValueObject source);

        public override partial void Map(PropertyShowInValueObject source, PropertyShowInDto destination);
    }

    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public partial class ModuleToModuleDtoMapper : MapperBase<Module, ModuleDto>
    {
        public override partial ModuleDto Map(Module source);

        public override partial void Map(Module source, ModuleDto destination);
    }
}
