using Volo.Abp.Reflection;

namespace EasyAbp.Abp.EntityUi.Permissions
{
    public class EntityUiPermissions
    {
        public const string GroupName = "EasyAbp.Abp.EntityUi";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(EntityUiPermissions));
        }
    }
}