using System;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateModalModel : EntityUiPageModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentEntity _currentEntity;
        private readonly IIntegrationAppService _integrationAppService;
        private readonly IEntityUiStringLocalizerProvider _stringLocalizerProvider;
        private IStringLocalizer StringLocalizer { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ModuleName { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public string EntityName { get; set; }
        
        public EntityDto Entity { get; set; }
        
        public EntityDto ParentEntity { get; set; }

        public bool IsSubEntity => !Entity.BelongsTo.IsNullOrEmpty();

        public object ViewModel { get; set; }
        
        public CreateModalModel(
            IServiceProvider serviceProvider,
            ICurrentEntity currentEntity,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
        {
            _serviceProvider = serviceProvider;
            _currentEntity = currentEntity;
            _integrationAppService = integrationAppService;
            _stringLocalizerProvider = stringLocalizerProvider;
        }

        public virtual async Task OnGetAsync()
        {
            var integration = await _integrationAppService.GetModuleAsync(ModuleName);

            var module = integration.Modules.Single(x => x.Name == ModuleName);

            Entity = integration.Entities.Single(x => x.Name == EntityName);
            
            _currentEntity.Set(module, Entity);

            if (IsSubEntity)
            {
                ParentEntity = integration.Entities.Single(x => x.Name == Entity.BelongsTo);
            }
            
            StringLocalizer = await _stringLocalizerProvider.GetAsync(module);

            ViewModel = Activator.CreateInstance(
                Type.GetType($"{Entity.CreationDtoTypeName}, {Entity.ContractsAssemblyName}")!);
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
        
        protected virtual async Task ProcessInputGroupAsync(TagHelperContext context, ModelExpression model)
        {
            var abpInputTagHelper = _serviceProvider.GetRequiredService<AbpInputTagHelper>();
            
            abpInputTagHelper.AspFor = model;
            // abpInputTagHelper.DisplayRequiredSymbol = TagHelper.RequiredSymbols ?? true;

            
            var output = await abpInputTagHelper.ProcessAndGetOutputAsync(new TagHelperAttributeList(), context, "div", TagMode.StartTagAndEndTag);
        }
    }
}