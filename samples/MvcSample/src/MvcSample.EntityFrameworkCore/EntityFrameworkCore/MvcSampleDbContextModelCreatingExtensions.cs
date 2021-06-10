using MvcSample.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

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


            builder.Entity<Book>(b =>
            {
                b.ToTable(MvcSampleConsts.DbTablePrefix + "Books", MvcSampleConsts.DbSchema);
                b.ConfigureByConvention(); 

                /* Configure more properties here */
            });

            builder.Entity<BookDetail>(b =>
            {
                b.ToTable(MvcSampleConsts.DbTablePrefix + "BookDetails", MvcSampleConsts.DbSchema);
                b.ConfigureByConvention(); 
                
                /* Configure more properties here */
            });

            builder.Entity<BookTag>(b =>
            {
                b.ToTable(MvcSampleConsts.DbTablePrefix + "BookTags", MvcSampleConsts.DbSchema);
                b.ConfigureByConvention(); 
                
                b.HasKey(e => new
                {
                    e.BookId,
                    e.Tag,
                });

                /* Configure more properties here */
            });
        }
    }
}
