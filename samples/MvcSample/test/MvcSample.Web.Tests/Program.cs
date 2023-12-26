using Microsoft.AspNetCore.Builder;
using MvcSample;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<MvcSampleWebTestModule>();

public partial class Program
{
}