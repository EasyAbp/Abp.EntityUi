using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using EntityDto = EasyAbp.Abp.EntityUi.Entities.Dtos.EntityDto;

namespace EasyAbp.Abp.EntityUi.Entities
{
    public class EntityAppService : AbstractKeyCrudAppService<Entity, EntityDto, EntityKey, PagedAndSortedResultRequestDto, CreateUpdateEntityDto, CreateUpdateEntityDto>,
        IEntityAppService
    {

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
            // TODO: AbpHelper generated
            return await AsyncExecuter.FirstOrDefaultAsync(
                _repository.Where(e =>
                    e.ModuleName == id.ModuleName &&
                    e.Name == id.Name
                )
            ); 
        }

        protected override IQueryable<Entity> ApplyDefaultSorting(IQueryable<Entity> query)
        {
            // TODO: AbpHelper generated
            return query.OrderBy(e => e.ModuleName);
        }
    }
}
