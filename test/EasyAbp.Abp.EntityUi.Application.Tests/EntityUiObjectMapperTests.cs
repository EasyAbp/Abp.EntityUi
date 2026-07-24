using System.Collections.Generic;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.Abp.EntityUi
{
    public class EntityUiObjectMapperTests : EntityUiApplicationTestBase
    {
        private readonly IObjectMapper<AbpEntityUiApplicationModule> _objectMapper;

        public EntityUiObjectMapperTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<AbpEntityUiApplicationModule>>();
        }

        [Fact]
        public void Should_Map_Entity_To_EntityDto()
        {
            // Arrange
            var entity = new Entity(new EntityDataModel(
                "TestModule", "TestEntity", "TestProvider", "TestNamespace", null, "{Id}",
                true, null, true, null, true, null, true, null,
                null, null, null, null, null, null, null, null, null, null, null, null, null,
                new List<Property>
                {
                    new Property("TestModule", "TestEntity", "Title", false, "System.String", false,
                        new PropertyShowInValueObject(true, true, false, true))
                }));

            // Act
            var dto = _objectMapper.Map<Entity, EntityDto>(entity);

            // Assert
            dto.ModuleName.ShouldBe("TestModule");
            dto.Name.ShouldBe("TestEntity");
            dto.ProviderName.ShouldBe("TestProvider");
            dto.Keys.ShouldBe("{Id}");
            dto.Properties.Count.ShouldBe(1);
            dto.Properties[0].Name.ShouldBe("Title");
            dto.Properties[0].TypeOrEntityName.ShouldBe("System.String");
            dto.Properties[0].ShowIn.List.ShouldBeTrue();
            dto.Properties[0].ShowIn.Creation.ShouldBeFalse();
        }

        [Fact]
        public void Should_Map_Module_To_ModuleDto()
        {
            // Arrange
            var module = new Module("TestModule", "ResourceType", "ResourceAssembly");

            // Act
            var dto = _objectMapper.Map<Module, ModuleDto>(module);

            // Assert
            dto.Name.ShouldBe("TestModule");
            dto.LResourceTypeName.ShouldBe("ResourceType");
            dto.LResourceTypeAssemblyName.ShouldBe("ResourceAssembly");
        }
    }
}
