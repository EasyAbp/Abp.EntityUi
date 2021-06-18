using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Integration.Dtos;
using EasyAbp.Abp.EntityUi.MenuItems.Dtos;
using EasyAbp.Abp.EntityUi.Modules.Dtos;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.Abp.EntityUi.Web.Menus
{
    public class EntityUiMenuContributor : IMenuContributor
    {
        private Dictionary<string, IStringLocalizer> ModuleNameStringLocalizers { get; } = new();
        
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        protected virtual async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var integrationAppService = context.ServiceProvider.GetRequiredService<IIntegrationAppService>();
            var stringLocalizerProvider = context.ServiceProvider.GetRequiredService<IEntityUiStringLocalizerProvider>();

            var integration = await integrationAppService.GetAsync();

            await AddMenuItemsAsync(context.Menu, integration.MenuItems, integration, stringLocalizerProvider, context);
        }

        protected virtual async Task AddMenuItemsAsync(IHasMenuItems parent, IEnumerable<MenuItemDto> menuItems,
            EntityUiIntegrationDto integration, IEntityUiStringLocalizerProvider stringLocalizerProvider,
            MenuConfigurationContext context)
        {
            foreach (var menuItem in menuItems)
            {
                if (menuItem.Permission != null && !await context.IsGrantedAsync(menuItem.Permission))
                {
                    continue;
                }
                
                var l = await GetOrCreateStringLocalizerAsync(
                    integration.Modules.First(x => x.Name == menuItem.ModuleName), stringLocalizerProvider);

                var child = new ApplicationMenuItem(menuItem.Name, l[menuItem.LocalizationItemName]);
                
                if (menuItem.MenuItems.IsNullOrEmpty())
                {
                    child.Url = $"/EntityUi/{menuItem.ModuleName}/{menuItem.EntityName}";
                }
                else
                {
                    await AddMenuItemsAsync(child, menuItem.MenuItems, integration, stringLocalizerProvider, context);
                }
                parent.Items.Add(child);
            }
        }

        protected virtual async Task<IStringLocalizer> GetOrCreateStringLocalizerAsync(ModuleDto module,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
        {
            if (ModuleNameStringLocalizers.ContainsKey(module.Name))
            {
                return ModuleNameStringLocalizers[module.Name];
            }

            var localizer =  await stringLocalizerProvider.GetAsync(module);

            ModuleNameStringLocalizers[module.Name] = localizer;

            return localizer;
        }
    }
}