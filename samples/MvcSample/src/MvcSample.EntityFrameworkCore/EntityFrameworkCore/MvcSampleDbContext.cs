using EasyAbp.Abp.DynamicEntity.EntityFrameworkCore;
using EasyAbp.Abp.DynamicMenu.EntityFrameworkCore;
using EasyAbp.Abp.DynamicPermission.EntityFrameworkCore;
using EasyAbp.Abp.EntityUi.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Identity;
using MvcSample.Books;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace MvcSample.EntityFrameworkCore
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ReplaceDbContext(typeof(ITenantManagementDbContext))]
    [ConnectionStringName("Default")]
    public class MvcSampleDbContext :
        AbpDbContext<MvcSampleDbContext>,
        IIdentityDbContext,
        ITenantManagementDbContext
    {
        /* Add DbSet properties for your Aggregate Roots / Entities here. */
        
        public DbSet<Book> Books { get; set; }
        
        public DbSet<BookDetail> BookDetails { get; set; }
        
        public DbSet<BookTag> BookTags { get; set; }
        
        #region Entities from the modules
        
        /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
         * and replaced them for this DbContext. This allows you to perform JOIN
         * queries for the entities of these modules over the repositories easily. You
         * typically don't need that for other modules. But, if you need, you can
         * implement the DbContext interface of the needed module and use ReplaceDbContext
         * attribute just like IIdentityDbContext and ITenantManagementDbContext.
         *
         * More info: Replacing a DbContext of a module ensures that the related module
         * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
         */
        
        //Identity
        public DbSet<IdentityUser> Users { get; set; }
        public DbSet<IdentityRole> Roles { get; set; }
        public DbSet<IdentityClaimType> ClaimTypes { get; set; }
        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
        public DbSet<IdentityLinkUser> LinkUsers { get; set; }
        public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
        public DbSet<IdentitySession> Sessions { get; set; }

        // Tenant Management
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

        #endregion

        public MvcSampleDbContext(DbContextOptions<MvcSampleDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */
            
            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentity();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureTenantManagement();
            
            builder.ConfigureAbpEntityUi();
            builder.ConfigureAbpDynamicPermission();
            builder.ConfigureAbpDynamicEntity();
            builder.ConfigureAbpDynamicMenu();
            
            /* Configure your own tables/entities inside here */

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
