using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace MvcSample.EntityFrameworkCore
{
    public static class MvcSampleDbContextModelCreatingExtensions
    {
        public static void ConfigureMvcSample(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(MvcSampleConsts.DbTablePrefix + "YourEntities", MvcSampleConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}