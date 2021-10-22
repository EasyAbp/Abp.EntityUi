using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.DynamicMenu.MenuItems;
using EasyAbp.Abp.DynamicMenu.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Entities;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.Modules.Dtos;

namespace EasyAbp.Abp.EntityUi.Integration
{
    public class IntegrationAppService : EntityUiAppServiceBase, IIntegrationAppService
    {
        private readonly IEntityRepository _entityRepository;
        private readonly IMenuItemNameCalculator _menuItemNameCalculator;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IModuleRepository _moduleRepository;

        public IntegrationAppService(
            IEntityRepository entityRepository,
            IMenuItemNameCalculator menuItemNameCalculator,
            IMenuItemRepository menuItemRepository,
            IModuleRepository moduleRepository)
        {
            _entityRepository = entityRepository;
            _menuItemNameCalculator = menuItemNameCalculator;
            _menuItemRepository = menuItemRepository;
            _moduleRepository = moduleRepository;
        }
        
        public virtual async Task<EntityUiIntegrationDto> GetAsync()
        {
            var modules = await _moduleRepository.GetListAsync();

            var entities = await _entityRepository.GetListAsync(true);

            var menuItems =
                await _menuItemRepository.GetListAsync(
                    x => modules.Select(y => _menuItemNameCalculator.GetName(y.Name)).Contains(x.Name), true);

            return new EntityUiIntegrationDto
            {
                Modules = ObjectMapper.Map<List<Module>, List<ModuleDto>>(modules),
                Entities = ObjectMapper.Map<List<Entity>, List<EntityDto>>(entities),
                MenuItems = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(menuItems)
            };
        }
        
        public virtual async Task<EntityUiIntegrationDto> GetModuleAsync(string moduleName)
        {
            var modules = new List<Module> {await _moduleRepository.GetAsync(x => x.Name == moduleName)};

            var entities = await _entityRepository.GetListInModuleAsync(moduleName, true);

            var menuItem = await _menuItemRepository.GetAsync(x => x.Name == _menuItemNameCalculator.GetName(moduleName));

            return new EntityUiIntegrationDto
            {
                Modules = ObjectMapper.Map<List<Module>, List<ModuleDto>>(modules),
                Entities = ObjectMapper.Map<List<Entity>, List<EntityDto>>(entities),
                MenuItems = ObjectMapper.Map<List<MenuItem>, List<MenuItemDto>>(new List<MenuItem> {menuItem})
            };
        }
    }
}
