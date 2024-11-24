using CSAbpInfra.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CSAbpInfra.Application
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(CSAbpInfraApplicationContractsModule)
    )]
    public class CSAbpInfraApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CSAbpInfraApplicationModule>();
        }
    }
}