using SedolChecker.Core.Models;
using StructureMap;

namespace SedolCheckerAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                host.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, services) =>
            {
                services.Configure<WeighingFactorConfig>(options=>context.Configuration.GetSection("WeighingFactorConfig").Bind(options));
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .UseServiceProviderFactory(c =>
            {
                return new StructureMapServiceProviderFactory(new ServiceRegistry(c.Configuration));
            });
      
    }
}
