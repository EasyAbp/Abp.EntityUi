using Microsoft.AspNetCore.Builder;
using MvcSample;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("MvcSample.Web.csproj");
await builder.RunAbpModuleAsync<MvcSampleWebTestModule>(applicationName: "MvcSample.Web" );

public partial class Program
{
}