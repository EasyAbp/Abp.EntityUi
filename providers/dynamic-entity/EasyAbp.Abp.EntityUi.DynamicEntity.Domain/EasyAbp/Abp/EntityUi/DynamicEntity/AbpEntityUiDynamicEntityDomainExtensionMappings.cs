using System;
using EasyAbp.Abp.EntityUi.Entities;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace EasyAbp.Abp.EntityUi.DynamicEntity
{
    public static class AbpEntityUiDynamicEntityDomainExtensionMappings
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                ObjectExtensionManager.Instance.AddOrUpdate<Entity>(options =>
                {
                    options.AddOrUpdateProperty<Guid?>(
                        AbpEntityUiDynamicEntityConsts.EntityModelDefinitionIdPropertyName);
                });
            });
        }
    }
}
