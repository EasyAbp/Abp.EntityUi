using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Encodings.Web;
using EasyAbp.Abp.EntityUi.Web.Pages.EntityUi;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(AbpDynamicFormTagHelperService))]
    // [ExposeServices(typeof(IAbpTagHelperService<AbpDynamicFormTagHelper>))]
    public class EntityUiAbpDynamicFormTagHelperService : AbpDynamicFormTagHelperService
    {
        private readonly ICurrentEntity _currentEntity;

        public EntityUiAbpDynamicFormTagHelperService(
            HtmlEncoder htmlEncoder,
            IHtmlGenerator htmlGenerator,
            IServiceProvider serviceProvider,
            IStringLocalizer<AbpUiResource> localizer,
            ICurrentEntity currentEntity) :
            base(htmlEncoder, htmlGenerator, serviceProvider, localizer)
        {
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
    }
}