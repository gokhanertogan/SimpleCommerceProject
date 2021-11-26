using Microsoft.Extensions.Logging;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;
using SimpleCommerceProject.Data.Models.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace SimpleCommerceProject.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);

            hostBuilder.MigrateDbContext<CommerceContext>((context, services) =>
            {
                var env = services.GetService<IWebHostEnvironment>();
                var logger = services.GetService<ILogger<CommerceContextSeed>>();

                new CommerceContextSeed().SeedAsync(context, logger).Wait();
            });

            hostBuilder.Run();
        }

        public static IWebHost CreateHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
        }
    }
}
