using Microsoft.EntityFrameworkCore;
using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Persistence;
using RoyWeller.LondonStockApi.Persistence.Repositories;

namespace RoyWeller.LondonStockApi.Tests.Translators;

public class StockRespositoryTests
{
    private DatabaseContext _dbContext;
    private StockRepository _respository;

    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase(databaseName: "database")
            .Options;

        _dbContext = new DatabaseContext(options);
        _respository = new StockRepository(_dbContext);

        var stocks = new[]
        {
            new Stock() { Ticker = "aaaaa" },
            new Stock() { Ticker = "bbbbb" }
        };

        await _dbContext.Stocks.AddRangeAsync(stocks);
        await _dbContext.SaveChangesAsync();
    }

    [Test]
    public async Task CreateMissingStocksAsync_WhenExists_DoesNotIncreaseStockCount()
    {
        //Act
        await _respository.CreateMissingStocksAsync(new[] { "aaaaa" });

        //Assert
        _dbContext.Stocks.ToList().Should().HaveCount(2);
    }
}