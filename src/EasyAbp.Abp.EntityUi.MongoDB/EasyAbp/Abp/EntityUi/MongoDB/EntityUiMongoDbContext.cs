using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.Abp.EntityUi.MongoDB
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public class EntityUiMongoDbContext : AbpMongoDbContext, IEntityUiMongoDbContext
    {
        /* Add mongo collections here. Example:
         * public IMongoCollection<Question> Questions => Collection<Question>();
         */

        protected override void CreateModel(IMongoModelBuilder modelBuilder)
        {
            base.CreateModel(modelBuilder);

            modelBuilder.ConfigureEntityUi();
        }
    }
}