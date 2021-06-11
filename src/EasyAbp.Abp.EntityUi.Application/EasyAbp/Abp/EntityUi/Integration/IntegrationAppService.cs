using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Integration
{
    public class IntegrationAppService : EntityUiAppServiceBase, IIntegrationAppService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IEntityRepository _entityRepository;
        private readonly IModuleRepository _moduleRepository;

        public IntegrationAppService(
            IMenuItemRepository menuItemRepository,
            IEntityRepository entityRepository,
            IModuleRepository moduleRepository)
        {
            _menuItemRepository = menuItemRepository;
            _entityRepository = entityRepository;
            _moduleRepository = moduleRepository;
        }
        
        public virtual async Task<EntityUiIntegrationDto> GetAsync()
        {
            var modules = await _moduleRepository.GetListAsync();

            var entities = await _entityRepository.GetListAsync(true);

            var menuItems = await _menuItemRepository.GetListAsync(null, true);

            return new EntityUiIntegrationDto
            {
                Modules = ObjectMapper.Map<List<Module>, List<ModuleDto>>(modules),
                Entities = ObjectMapper.Map<List<Entity>, List<EntityDto>>(entities),
                MenuItems = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(menuItems)
            };
        }
    }
}
