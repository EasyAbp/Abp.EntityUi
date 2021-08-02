using System;
using System.Threading.Tasks;
using EasyAbp.Abp.EntityUi.Entities.Dtos;
using EasyAbp.Abp.EntityUi.Integration;
using EasyAbp.Abp.EntityUi.Web.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Volo.Abp.Json;

namespace EasyAbp.Abp.EntityUi.Web.Pages.EntityUi
{
    public class CreateModalModelModel : EntityUiModalModelBase
    {
        private readonly ICurrentEntity _currentEntity;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IEntityUiStringLocalizerProvider _stringLocalizerProvider;
        private IStringLocalizer StringLocalizer { get; set; }

        public CreateModalModelModel(
            ICurrentEntity currentEntity,
            IJsonSerializer jsonSerializer,
            IServiceProvider serviceProvider,
            IIntegrationAppService integrationAppService,
            IEntityUiStringLocalizerProvider stringLocalizerProvider)
            : base(currentEntity, jsonSerializer, serviceProvider, integrationAppService)
        {
            _currentEntity = currentEntity;
            _jsonSerializer = jsonSerializer;
            _stringLocalizerProvider = stringLocalizerProvider;
        }

        public virtual async Task OnGetAsync()
        {
            await SetCurrentEntityAsync();

            StringLocalizer = await _stringLocalizerProvider.GetAsync(_currentEntity.GetModule());

            ViewModel = Activator.CreateInstance(
                Type.GetType($"{Entity.CreationDtoTypeName}, {Entity.ContractsAssemblyName}")!);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await SetCurrentEntityAsync();

            var entity = _currentEntity.GetEntity();

            var appService = GetAppService();

            var json = MapFormToDtoJsonString();
            
            dynamic task =
                GetAppServiceType().GetInheritedMethod(entity.AppServiceCreateMethodName)!.Invoke(appService,
                    new[] {_jsonSerializer.Deserialize(entity.GetAppServiceCreationDtoType(), json)});

            if (task != null)
            {
                await task;
            }

            return NoContent();
        }

        public virtual Task<string> GetModalTitleAsync()
        {
            return Task.FromResult<string>(StringLocalizer[$"Create{Entity.Name}"]);
        }
    }
}