using EasyAbp.Abp.EntityUi.Modules;
using EasyAbp.Abp.EntityUi.MenuItems;
using EasyAbp.Abp.EntityUi.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.Abp.EntityUi.EntityFrameworkCore
{
    public static class EntityUiDbContextModelCreatingExtensions
    {
        public static void ConfigureAbpEntityUi(
            this ModelBuilder builder,
            Action<EntityUiModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new EntityUiModelBuilderConfigurationOptions(
                EntityUiDbProperties.DbTablePrefix,
                EntityUiDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */

            builder.Entity<Entity>(b =>
            {
                b.ToTable(options.TablePrefix + "Entities", options.Schema);
                b.ConfigureByConvention(); 
                
                b.HasKey(e => new
                {
                    e.ModuleName,
                    e.Name,
                });

                /* Configure more properties here */
                b.Property(x => x.Keys).HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
            });
            
            builder.Entity<Property>(b =>
            {
                b.ToTable(options.TablePrefix + "Properties", options.Schema);
                b.ConfigureByConvention(); 
                
                b.HasKey(e => new
                {
                    e.EntityModuleName,
                    e.EntityName,
                    e.Name,
                });

                /* Configure more properties here */
                b.OwnsOne(x => x.ShowIn);
            });

            builder.Entity<MenuItem>(b =>
            {
                b.ToTable(options.TablePrefix + "MenuItems", options.Schema);
                b.ConfigureByConvention(); 
                
                b.HasKey(e => new
                {
                    e.Name,
                });

                /* Configure more properties here */
            });

            builder.Entity<Module>(b =>
            {
                b.ToTable(options.TablePrefix + "Modules", options.Schema);
                b.ConfigureByConvention(); 
                
                b.HasKey(e => new
                {
                    e.Name,
                });

                /* Configure more properties here */
            });
        }
    }
}
