﻿using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.Abp.EntityUi.MongoDB
{
    [ConnectionStringName(EntityUiDbProperties.ConnectionStringName)]
    public interface IEntityUiMongoDbContext : IAbpMongoDbContext
    {
        /* Define mongo collections here. Example:
         * IMongoCollection<Question> Questions { get; }
         */
    }
}
