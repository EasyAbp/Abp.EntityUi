using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Web.Pages.EntityUi;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Extensions;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(AbpDynamicFormTagHelperService))]
    public class EntityUiAbpDynamicFormTagHelperService : AbpDynamicFormTagHelperService
    {
        private readonly HtmlEncoder _htmlEncoder;
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentEntity _currentEntity;

        public EntityUiAbpDynamicFormTagHelperService(
            HtmlEncoder htmlEncoder,
            IHtmlGenerator htmlGenerator,
            IServiceProvider serviceProvider,
            IStringLocalizer<AbpUiResource> localizer,
            ICurrentEntity currentEntity) :
            base(htmlEncoder, htmlGenerator, serviceProvider, localizer)
        {
            _htmlEncoder = htmlEncoder;
            _serviceProvider = serviceProvider;
            _currentEntity = currentEntity;
        }

        protected override List<ModelExpression> ExploreModelsRecursively(List<ModelExpression> list, ModelExplorer model)
        {
            var entity = _currentEntity.GetEntity();

            if (entity.Properties.Any(x => !x.ShowIn.Creation && x.Name == model.Metadata.PropertyName))
            {
                return list;
            }

            if (IsListOfClass(model.ModelType))
            {
                return list;
            }
            
            return base.ExploreModelsRecursively(list, model);
        }

        protected virtual bool IsListOfClass(Type type)
        {
            if (IsCsharpClassOrPrimitive(type))
            {
                return false;
            }
            
            var genericType = type.GenericTypeArguments.FirstOrDefault();

            if (genericType == null)
            {
                return false;
            }
            
            return type.ToString().StartsWith("System.Collections.Generic.IEnumerable`") || type.ToString().StartsWith("System.Collections.Generic.List`");
        }
        
        protected override async Task ProcessInputGroupAsync(TagHelperContext context, TagHelperOutput output, ModelExpression model)
        {
            var abpInputTagHelper = _serviceProvider.GetRequiredService<AbpInputTagHelper>();
            abpInputTagHelper.AspFor = model;
            abpInputTagHelper.ViewContext = TagHelper.ViewContext;
            abpInputTagHelper.DisplayRequiredSymbol = TagHelper.RequiredSymbols ?? true;
            
            // Todo: Wait for https://github.com/abpframework/abp/issues/9723
            abpInputTagHelper.CheckBoxHiddenInputRenderMode = CheckBoxHiddenInputRenderMode.EndOfForm;

            await abpInputTagHelper.RenderAsync(new TagHelperAttributeList(), context, _htmlEncoder, "div", TagMode.StartTagAndEndTag);
        }
    }
}