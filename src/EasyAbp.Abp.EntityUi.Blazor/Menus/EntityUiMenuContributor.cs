using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.Abp.EntityUi.Blazor.Menus
{
    public class EntityUiMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(EntityUiMenus.Prefix, displayName: "EntityUi", "/EntityUi", icon: "fa fa-globe"));
            
            return Task.CompletedTask;
        }
    }
}