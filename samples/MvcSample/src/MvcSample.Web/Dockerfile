#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["samples/MvcSample/NuGet.Config", "samples/MvcSample/"]
COPY ["NuGet.Config", "."]
COPY ["samples/MvcSample/src/MvcSample.Web/MvcSample.Web.csproj", "samples/MvcSample/src/MvcSample.Web/"]
COPY ["src/EasyAbp.Abp.EntityUi.Web/EasyAbp.Abp.EntityUi.Web.csproj", "src/EasyAbp.Abp.EntityUi.Web/"]
COPY ["src/EasyAbp.Abp.EntityUi.HttpApi/EasyAbp.Abp.EntityUi.HttpApi.csproj", "src/EasyAbp.Abp.EntityUi.HttpApi/"]
COPY ["src/EasyAbp.Abp.EntityUi.Application.Contracts/EasyAbp.Abp.EntityUi.Application.Contracts.csproj", "src/EasyAbp.Abp.EntityUi.Application.Contracts/"]
COPY ["src/EasyAbp.Abp.EntityUi.Domain.Shared/EasyAbp.Abp.EntityUi.Domain.Shared.csproj", "src/EasyAbp.Abp.EntityUi.Domain.Shared/"]
COPY ["samples/MvcSample/src/MvcSample.HttpApi/MvcSample.HttpApi.csproj", "samples/MvcSample/src/MvcSample.HttpApi/"]
COPY ["samples/MvcSample/src/MvcSample.Application.Contracts/MvcSample.Application.Contracts.csproj", "samples/MvcSample/src/MvcSample.Application.Contracts/"]
COPY ["samples/MvcSample/src/MvcSample.Domain.Shared/MvcSample.Domain.Shared.csproj", "samples/MvcSample/src/MvcSample.Domain.Shared/"]
COPY ["providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared.csproj", "providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.Shared/"]
COPY ["samples/MvcSample/src/MvcSample.Application/MvcSample.Application.csproj", "samples/MvcSample/src/MvcSample.Application/"]
COPY ["samples/MvcSample/src/MvcSample.Domain/MvcSample.Domain.csproj", "samples/MvcSample/src/MvcSample.Domain/"]
COPY ["providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Domain/EasyAbp.Abp.EntityUi.DynamicEntity.Domain.csproj", "providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Domain/"]
COPY ["src/EasyAbp.Abp.EntityUi.Domain/EasyAbp.Abp.EntityUi.Domain.csproj", "src/EasyAbp.Abp.EntityUi.Domain/"]
COPY ["src/EasyAbp.Abp.EntityUi.Application/EasyAbp.Abp.EntityUi.Application.csproj", "src/EasyAbp.Abp.EntityUi.Application/"]
COPY ["providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Web/EasyAbp.Abp.EntityUi.DynamicEntity.Web.csproj", "providers/dynamic-entity/EasyAbp.Abp.EntityUi.DynamicEntity.Web/"]
COPY ["samples/MvcSample/src/MvcSample.EntityFrameworkCore/MvcSample.EntityFrameworkCore.csproj", "samples/MvcSample/src/MvcSample.EntityFrameworkCore/"]
COPY ["src/EasyAbp.Abp.EntityUi.EntityFrameworkCore/EasyAbp.Abp.EntityUi.EntityFrameworkCore.csproj", "src/EasyAbp.Abp.EntityUi.EntityFrameworkCore/"]
COPY Directory.Build.props .
RUN dotnet restore "samples/MvcSample/src/MvcSample.Web/MvcSample.Web.csproj"
COPY . .
WORKDIR "/src/samples/MvcSample/src/MvcSample.Web"
RUN dotnet build "MvcSample.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MvcSample.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MvcSample.Web.dll"]
