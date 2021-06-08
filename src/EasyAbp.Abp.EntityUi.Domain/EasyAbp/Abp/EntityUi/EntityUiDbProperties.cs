namespace EasyAbp.Abp.EntityUi
{
    public static class EntityUiDbProperties
    {
        public static string DbTablePrefix { get; set; } = "EasyAbpAbpEntityUi";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "EasyAbpAbpEntityUi";
    }
}
