using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Localization;
using EasyAbp.Abp.EntityUi.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EntityDto = EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class EntityAppService : AbstractKeyCrudAppService<Entity, EntityDto, EntityKey, PagedAndSortedResultRequestDto, CreateUpdateEntityDto, CreateUpdateEntityDto>,
        IEntityAppService
    {
        protected override string GetPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string GetListPolicyName { get; set; } = EntityUiPermissions.GroupName;
        protected override string CreatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string UpdatePolicyName { get; set; } = EntityUiPermissions.Manage;
        protected override string DeletePolicyName { get; set; } = EntityUiPermissions.Manage;

        private readonly IEntityRepository _repository;
        
        public EntityAppService(IEntityRepository repository) : base(repository)
        {
            _repository = repository;

            LocalizationResource = typeof(EntityUiResource);
            ObjectMapperContext = typeof(AbpEntityUiApplicationModule);
        }
        
        protected override Task DeleteByIdAsync(EntityKey id)
        {
            // TODO: AbpHelper generated
            return _repository.DeleteAsync(e =>
                e.ModuleName == id.ModuleName &&
                e.Name == id.Name
            );
        }

        protected override async Task<Entity> GetEntityByIdAsync(EntityKey id)
        {
            return await _repository.GetAsync(e =>
                    e.ModuleName == id.ModuleName &&
                    e.Name == id.Name
                ); 
        }

        protected override IQueryable<Entity> ApplyDefaultSorting(IQueryable<Entity> query)
        {
            // TODO: AbpHelper generated
            return query.OrderBy(e => e.ModuleName);
        }

        protected override Task<Entity> MapToEntityAsync(CreateUpdateEntityDto createInput)
        {
            return Task.FromResult(new Entity(CreateEntityDataModel(createInput)));
        }

        protected override Task MapToEntityAsync(CreateUpdateEntityDto updateInput, Entity entity)
        {
            entity.Update(CreateEntityDataModel(updateInput));
            return Task.CompletedTask;
        }

        private static EntityDataModel CreateEntityDataModel(CreateUpdateEntityDto input)
        {
            return new EntityDataModel(
                input.ModuleName, input.Name, input.ProviderName, input.Namespace, input.BelongsTo, input.Keys,
                input.CreationEnabled, input.CreationPermission, input.EditEnabled, input.EditPermission,
                input.DeletionEnabled, input.DeletionPermission, input.DetailEnabled, input.DetailPermission,
                input.ContractsAssemblyName, input.ListItemDtoTypeName, input.DetailDtoTypeName,
                input.CreationDtoTypeName, input.EditDtoTypeName, input.GetListInputDtoTypeName,
                input.KeyClassTypeName, input.AppServiceInterfaceName, input.AppServiceGetListMethodName,
                input.AppServiceGetMethodName, input.AppServiceCreateMethodName, input.AppServiceUpdateMethodName,
                input.AppServiceDeleteMethodName,
                input.Properties.Select(MapToProperty).ToList());
        }

        private static Property MapToProperty(CreateUpdatePropertyDto input)
        {
            return new Property(
                input.EntityModuleName, input.EntityName, input.Name, input.IsEntityCollection,
                input.TypeOrEntityName, input.Nullable,
                new PropertyShowInValueObject(
                    input.ShowIn.List, input.ShowIn.Detail, input.ShowIn.Creation, input.ShowIn.Edit));
        }
    }
}
