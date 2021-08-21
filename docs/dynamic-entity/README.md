# Abp.EntityUi.DynamicEntity

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.EntityUi%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.EntityUi?style=social)](https://www.github.com/EasyAbp/Abp.EntityUi)

Dynamic-entity implementation of the EasyAbp.Abp.EntityUi.DynamicEntity module.

## Online Demo

We have launched an online demo for this module: [https://entityui.samples.easyabp.io](https://entityui.samples.easyabp.io)

## Installation

> Should also install the [Abp.EntityUI](/docs/README.md#installation) module and the [Abp.DynamicEntity](https://github.com/EasyAbp/Abp.DynamicEntity) module since this module depends on them.

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.Abp.EntityUi.DynamicEntity.Domain
    * EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared
    * EasyAbp.Abp.EntityUi.DynamicEntity.Web

1. Add `DependsOn(typeof(AbpEntityUiDynamicEntityXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

## Usage

There is a [handler](https://github.com/EasyAbp/Abp.EntityUi/blob/main/providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Domain/EasyAbp/Abp/EntityUi/DynamicEntity/EventHandlers/DynamicEntityModelDefinitionEventHandler.cs) that automatically handles ModelDefinition created/updated/deleted events of DynamicEntity module to create/update/delete entity definition in the EntityUi.