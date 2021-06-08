﻿using EasyAbp.Abp.EntityUi.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.Abp.EntityUi.Permissions
{
    public class EntityUiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(EntityUiPermissions.GroupName, L("Permission:EntityUi"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<EntityUiResource>(name);
        }
    }
}