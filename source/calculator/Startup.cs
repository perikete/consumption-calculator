using CalculatorFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CalculatorFunctions.Startup))]
namespace CalculatorFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddLogging()
                .AddSingleton<IMeterDataParser, MeterDataParser>()
                .AddSingleton<IMeterResultStore, InMemoryMeterResultStore>();
        }
    }
}
