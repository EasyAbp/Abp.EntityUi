using System.Collections.Generic;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using AutoMapper;

namespace EasyAbp.Abp.EntityUi
{
    public class EntityUiApplicationAutoMapperProfile : Profile
    {
        public EntityUiApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Entity, EntityDto>()
                .ForMember(x => x.Keys, opts => opts.MapFrom(x => x.Keys.JoinAsString(",")));
            CreateMap<CreateUpdateEntityDto, Entity>(MemberList.Source)
                .ForMember(x => x.Keys, opts => opts.MapFrom(x => x.Keys.Split(',')));;
            CreateMap<Property, PropertyDto>();
            CreateMap<CreateUpdatePropertyDto, Property>(MemberList.Source);
            CreateMap<PropertyShowInDto, PropertyShowInValueObject>();
            CreateMap<PropertyShowInValueObject, PropertyShowInDto>();
            CreateMap<MenuItem, MenuItemDto>();
            CreateMap<CreateUpdateMenuItemDto, MenuItem>(MemberList.Source);
            CreateMap<Module, ModuleDto>();
            CreateMap<CreateUpdateModuleDto, Module>(MemberList.Source);
        }
    }
}
