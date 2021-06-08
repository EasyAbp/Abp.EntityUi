namespace MvcSample.Permissions
{
    public static class MvcSamplePermissions
    {
        public const string GroupName = "MvcSample";

        //Add your own permission names. Example:
        //public const string MyPermission1 = GroupName + ".MyPermission1";

        public class Book
        {
            public const string Default = GroupName + ".Book";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}
