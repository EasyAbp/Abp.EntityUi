using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.EntityUi.Web.Infrastructures
{
    public class NatashaViewModelTypeProvider : IViewModelTypeProvider, ISingletonDependency
    {
        private const string DomainKeyPrefix = "AbpDynamicEntity";

        private readonly Dictionary<string, NatashaViewModelTypeModel> _entityFullNameViewModelTypeMapping = new();

        private readonly ConcurrentDictionary<string, SemaphoreSlim> _asyncLocks = new();

        public virtual async Task<Type> GetCreationViewModelTypeAsync(EntityDto entityDto)
        {
            return (await GetTypeModelAsync(entityDto)).CreationViewModelType;
        }

        public virtual async Task<Type> GetEditViewModelTypeAsync(EntityDto entityDto)
        {
            return (await GetTypeModelAsync(entityDto)).EditViewModelType;
        }

        protected virtual Task<NatashaLoadContext> RecreateAndGetDomainAsync(EntityDto entityDto)
        {
            var domainKey = $"{DomainKeyPrefix}_{entityDto.GetFullName()}";

            if (DomainManagement.Get(domainKey) != null)
            {
                DomainManagement.Remove(domainKey);
            }

            return Task.FromResult(DomainManagement.Create(domainKey));
        }

        protected virtual async Task<NatashaViewModelTypeModel> GetTypeModelAsync(EntityDto entityDto)
        {
            var key = entityDto.GetFullName();

            var model = _entityFullNameViewModelTypeMapping.ContainsKey(key)
                ? _entityFullNameViewModelTypeMapping[key]
                : null;

            if (model != null && model.EntityLastModificationTime == entityDto.LastModificationTime)
            {
                return model;
            }

            var asyncLock = _asyncLocks.GetOrAdd(key, new SemaphoreSlim(1));

            await asyncLock.WaitAsync();

            try
            {
                if (_entityFullNameViewModelTypeMapping.ContainsKey(key) &&
                    _entityFullNameViewModelTypeMapping[key].EntityLastModificationTime ==
                    entityDto.LastModificationTime)
                {
                    return _entityFullNameViewModelTypeMapping[key];
                }

                var domain = await RecreateAndGetDomainAsync(entityDto);

                model = new NatashaViewModelTypeModel
                {
                    CreationViewModelType = await CreateCreationViewModelTypeAsync(entityDto, domain),
                    EditViewModelType = await CreateEditViewModelTypeAsync(entityDto, domain),
                    EntityLastModificationTime = entityDto.LastModificationTime
                };

                _entityFullNameViewModelTypeMapping[key] = model;
            }
            finally
            {
                asyncLock.Release();
            }

            return model;
        }

        protected virtual Task<Type> CreateCreationViewModelTypeAsync(EntityDto entityDto,
            NatashaLoadContext context)
        {
            var nClass = NClass.UseDomain(context);

            nClass
                .Namespace("EasyAbp.Abp.EntityUi.Web.Pages.EntityUi")
                .Public()
                .Name($"DynamicEntityCreate{entityDto.Name}Dto")
                .Ctor(ctor => ctor.Public().Body(string.Empty));

            foreach (var property in entityDto.Properties.Where(x => x.ShowIn.Creation))
            {
                nClass.Property(prop => prop
                    .Type(Type.GetType(property.TypeOrEntityName) ?? throw new InvalidOperationException())
                    .Name(property.Name)
                    .Public());
            }

            return Task.FromResult(nClass.GetType());
        }

        protected virtual Task<Type> CreateEditViewModelTypeAsync(EntityDto entityDto, NatashaLoadContext context)
        {
            var nClass = NClass.UseDomain(context);

            nClass
                .Namespace("EasyAbp.Abp.EntityUi.Web.Pages.EntityUi")
                .Public()
                .Name($"DynamicEntityUpdate{entityDto.Name}Dto")
                .Ctor(ctor => ctor.Public().Body(string.Empty));

            foreach (var property in entityDto.Properties.Where(x => x.ShowIn.Edit))
            {
                nClass.Property(prop => prop
                    .Type(Type.GetType(property.TypeOrEntityName))
                    .Name(property.Name)
                    .Public());
            }

            return Task.FromResult(nClass.GetType());
        }
    }
}