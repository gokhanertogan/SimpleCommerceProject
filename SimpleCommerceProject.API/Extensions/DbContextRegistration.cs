using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleCommerceProject.Data.Models.Infrastructure.Context;
using System;
using System.Reflection;

namespace SimpleCommerceProject.API.Extensions
{
    public static class DbContextRegistration
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<CommerceContext>(options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("CommerceConnectionString"),
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        });

                });

            return services;
        }
    }
}
