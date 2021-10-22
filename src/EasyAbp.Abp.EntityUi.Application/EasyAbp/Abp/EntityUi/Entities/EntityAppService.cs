using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
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
    }
}
