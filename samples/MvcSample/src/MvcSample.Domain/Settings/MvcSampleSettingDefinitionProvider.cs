using Volo.Abp.Settings;

namespace MvcSample.Settings
{
    public class MvcSampleSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(MvcSampleSettings.MySetting1));
        }
    }
}
