# Abp.EntityUi

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.EntityUi%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.EntityUi.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.EntityUi.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.EntityUi.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.EntityUi.Domain.Shared)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.EntityUi?style=social)](https://www.github.com/EasyAbp/Abp.EntityUi)

An abp module that dynamically generates management UI for entities in runtime.

## Online Demo

We have launched an online demo for this module: [https://entityui.samples.easyabp.io](https://entityui.samples.easyabp.io)

## Installation

1. Follow [the document](https://github.com/EasyAbp/Abp.DynamicMenu#installation) to install the dependent Abp.DynamicMenu module.

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.EntityUi.Application
    * EasyAbp.Abp.EntityUi.Application.Contracts
    * EasyAbp.Abp.EntityUi.Domain
    * EasyAbp.Abp.EntityUi.Domain.Shared
    * EasyAbp.Abp.EntityUi.EntityFrameworkCore
    * EasyAbp.Abp.EntityUi.HttpApi
    * EasyAbp.Abp.EntityUi.HttpApi.Client
    * EasyAbp.Abp.EntityUi.Web

1. Add `DependsOn(typeof(AbpEntityUiXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureAbpEntityUi();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

1. Configure for the modules (or an app itself) you want to use EntityUi.
    ```csharp
    public class MyProjectDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // ...
   
            Configure<AbpEntityUiOptions>(options =>
            {
                options.Modules.Add("MyProject", new AbpEntityUiModuleOptions(typeof(MyProjectDomainModule)));
            });
   
            // ...
        }
    }
    ```

2. Run the DbMigrator project, the EntityUi [seed contributor](https://github.com/EasyAbp/Abp.EntityUi/blob/main/src/EasyAbp.Abp.EntityUi.Domain/EasyAbp/Abp/EntityUi/Data/AbpEntityUiDataSeedContributor.cs) will discovery the entities and seed the metadata.

3. Run the app and log in as the admin user, you can see the "Entity UI" menu item, try to open it and change some entity configuration.

   ![EntityList](/docs/images/EntityList.png)

4. Use the management pages for the entities you just configured:

   ![BookList](/docs/images/BookList.png)
   ![EditBook](/docs/images/EditBook.png)

## Q&A

### How to package the EntityUi configurations for a module

1. Export the configurations with the http API: `/api/abp/entity-ui/integration/module/{moduleName}` (GET).

2. Create a JSON file like `EntityUiSeed.json` in your module project to save the exported data (see the [demo](https://github.com/EasyAbp/Abp.EntityUi/blob/main/src/EasyAbp.Abp.EntityUi.Domain/EasyAbp/Abp/EntityUi/EntityUiSeed.json)) and set it as an EmbeddedResource.

3. Configure the EntityUi to use it:
```csharp
    public class MyProjectDomainModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpEntityUiOptions>(options =>
            {
                options.Modules.Add(
                    "MyProject",
                    new AbpEntityUiModuleOptions(typeof(MyProjectDomainModule), "/EntityUiSeed.json")
                );
            });
            
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<MyProjectDomainModule>();
            });
        }
    }
```

### Entity UI for dynamic entities?

See the Entity UI dynamic entity provider [document](/docs/dynamic-entity/README.md).

## Road map

- [ ] Detail modal.
- [x] Support dynamic entities.
- [x] Support the MVC template.
- [ ] Support the Blazor template.
- [ ] Support the Angular template.
