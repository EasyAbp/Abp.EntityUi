using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateModalModel : EntityUiPageModel
    {
        private readonly IIntegrationAppService _integrationAppService;
        private readonly IEntityUiStringLocalizerProvider _stringLocalizerProvider;
        private IStringLocalizer StringLocalizer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public EntityDto Entity { get; set; }

        public CreateModalModel(
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
        {
            _integrationAppService = integrationAppService;
            _stringLocalizerProvider = stringLocalizerProvider;
        }

        public virtual async Task OnGetAsync()
        {
            var integration = await _integrationAppService.GetModuleAsync(ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            
            var module = integration.Modules.Single(x => x.Name == ModuleName);

            StringLocalizer = await _stringLocalizerProvider.GetAsync(module);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            // Todo: call app service.
            return NoContent();
        }

        public virtual Task<string> GetModalTitleAsync()
        {
            return Task.FromResult<string>(StringLocalizer[$"Create{Entity.Name}"]);
        }
    }
}