using MvcSample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace MvcSample.Permissions
{
    public class MvcSamplePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(MvcSamplePermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(MvcSamplePermissions.MyPermission1, L("Permission:MyPermission1"));

            var bookPermission = myGroup.AddPermission(MvcSamplePermissions.Book.Default, L("Permission:Book"));
            bookPermission.AddChild(MvcSamplePermissions.Book.Create, L("Permission:Create"));
            bookPermission.AddChild(MvcSamplePermissions.Book.Update, L("Permission:Update"));
            bookPermission.AddChild(MvcSamplePermissions.Book.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<MvcSampleResource>(name);
        }
    }
}
