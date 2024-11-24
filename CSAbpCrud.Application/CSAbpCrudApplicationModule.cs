using CSAbpCrud.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace CSAbpCrud.Application
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(CSAbpCrudApplicationContractsModule)
    )]
    public class CSAbpCrudApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAutoMapperObjectMapper<CSAbpCrudApplicationModule>();
        }
    }
}