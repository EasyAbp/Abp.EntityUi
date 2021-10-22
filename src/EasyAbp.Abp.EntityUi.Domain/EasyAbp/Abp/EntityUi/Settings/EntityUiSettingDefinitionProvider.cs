using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.EntityUi.Settings
{
    public class EntityUiSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /* Define module settings here.
             * Use names from EntityUiSettings class.
             */

            context.Add(new SettingDefinition(
                EntityUiSettings.AutoCreateDynamicMenuItem,
                true.ToString(),
                L("AutoCreateDynamicMenuItem")));
        }
        
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EntityUiResource>(name);
        }
    }
}