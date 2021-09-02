using System;
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
using Microsoft.Extensions.Logging;
using Volo.Abp.Authorization;
using Volo.Abp.Security.Claims;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.Abp.EntityUi.Web.Menus
{
    public class EntityUiMenuContributor : IMenuContributor
    {
        private ILogger<EntityUiMenuContributor> _logger;
        private IAbpAuthorizationPolicyProvider _policyProvider;
        private ICurrentPrincipalAccessor _currentPrincipalAccessor;
        private IIntegrationAppService _integrationAppService;
        private IEntityUiStringLocalizerProvider _stringLocalizerProvider;

        private Dictionary<string, IStringLocalizer> ModuleNameStringLocalizers { get; } = new();
        
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            var loggerFactory = context.ServiceProvider.GetRequiredService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<EntityUiMenuContributor>();
            _policyProvider = context.ServiceProvider.GetRequiredService<IAbpAuthorizationPolicyProvider>();
            _currentPrincipalAccessor = context.ServiceProvider.GetRequiredService<ICurrentPrincipalAccessor>();
            _integrationAppService = context.ServiceProvider.GetRequiredService<IIntegrationAppService>();
            _stringLocalizerProvider = context.ServiceProvider.GetRequiredService<IEntityUiStringLocalizerProvider>();
            
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        protected virtual async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var integration = await _integrationAppService.GetAsync();

            await AddMenuItemsAsync(context.Menu, integration.MenuItems, integration, context);
        }

        protected virtual async Task AddMenuItemsAsync(IHasMenuItems parent, IEnumerable<MenuItemDto> menuItems,
            EntityUiIntegrationDto integration, MenuConfigurationContext context)
        {
            foreach (var menuItem in menuItems)
            {
                if (menuItem.Permission != null && !await IsFoundAndGrantedAsync(menuItem.Permission, context))
                {
                    continue;
                }

                var l = await GetOrCreateStringLocalizerAsync(
                    integration.Modules.First(x => x.Name == menuItem.ModuleName), _stringLocalizerProvider);

                var child = new ApplicationMenuItem(menuItem.Name, l[menuItem.LocalizationItemName]);

                if (menuItem.MenuItems.IsNullOrEmpty())
                {
                    if (menuItem.ParentName.IsNullOrEmpty())
                    {
                        continue;
                    }

                    child.Url = $"/EntityUi/{menuItem.ModuleName}/{menuItem.EntityName}";
                }
                else
                {
                    await AddMenuItemsAsync(child, menuItem.MenuItems, integration, context);
                }

                parent.Items.Add(child);
            }
        }

        protected virtual async Task<bool> IsFoundAndGrantedAsync(string policyName, MenuConfigurationContext context)
        {
            if (policyName == null)
            {
                throw new ArgumentNullException(nameof(policyName));
            }

            var policy = await _policyProvider.GetPolicyAsync(policyName);
            
            if (policy == null)
            {
                _logger.LogWarning($"[Entity UI] No policy found: {policyName}.");
                
                return false;
            }
            
            return (await context.AuthorizationService.AuthorizeAsync(
                _currentPrincipalAccessor.Principal,
                null,
                policyName)).Succeeded;
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