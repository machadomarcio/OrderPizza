using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using OrderPizza.Data.Contexts;

namespace OrderPizza.Tests
{
    public class OrderPizzaAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        private readonly InMemoryDatabaseRoot _databaseRoot = new InMemoryDatabaseRoot();
        private readonly string _connectionString = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {

            builder.ConfigureServices(services =>
            {
                services
                    .AddEntityFrameworkInMemoryDatabase()
                    .AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase(_connectionString, _databaseRoot);
                        options.UseInternalServiceProvider(services.BuildServiceProvider());
                    });
            });

            builder.UseStartup<TStartup>();
            builder.UseEnvironment("Development");
        }
    }
}
