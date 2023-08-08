using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RoyWeller.LondonStockApi.Persistence;
using RoyWeller.LondonStockApi.Persistence.Repositories;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using RoyWeller.LondonStockApi.Services;
using RoyWeller.LondonStockApi.Services.Interfaces;
using RoyWeller.LondonStockApi.Utilities;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: FunctionsStartup(typeof(RoyWeller.LondonStockApi.Startup))]
[assembly: InternalsVisibleTo("RoyWeller.LondonStockApi.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace RoyWeller.LondonStockApi;
public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var logger = builder.Services.FirstOrDefault(s => s.ServiceType == typeof(ILogger<>));
        if (logger != null)
            builder.Services.Remove(logger);

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var dbPath = System.IO.Path.Join(path, "stockapi.db");

        builder.Services.Add(new ServiceDescriptor(typeof(ILogger<>), typeof(FunctionLogger<>), ServiceLifetime.Transient));
        builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlite($"Data Source={dbPath}"), ServiceLifetime.Singleton);
        builder.Services.AddSingleton<IBrokerRespository, BrokerRepository>();
        builder.Services.AddSingleton<IStockRepository, StockRepository>();
        builder.Services.AddSingleton<ITradeRepository, TradeRepository>();
        builder.Services.AddSingleton<IStockService, StockService>();
        builder.Services.AddSingleton<ITradeService, TradeService>();
        builder.Services.AddSingleton<ITradeValidationService, TradeValidationService>();
    }
}
