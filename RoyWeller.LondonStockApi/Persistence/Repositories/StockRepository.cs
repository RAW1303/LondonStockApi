using Microsoft.EntityFrameworkCore;
using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories;
internal class StockRepository : BaseRespository<Stock>, IStockRepository
{
    public StockRepository(DatabaseContext databaseContext) : base(databaseContext) { }

    public async Task CreateMissingStocksAsync(IEnumerable<string> tickers)
    {
        var currentStocks = await _dbSet
            .Where(s => tickers.Contains(s.Ticker))
            .AsNoTracking()
            .ToListAsync();

        var missingStocks = tickers
            .Where(t => !currentStocks.Any(s => s.Ticker == t)).Select(t => new Stock() { Ticker = t });

        if (!missingStocks.Any())
            return;

        await _dbSet.AddRangeAsync(missingStocks);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Stock>> GetStocksAsync()
    {
        return await _dbSet
            .Include(t => t.Trades)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Stock>> GetStocksAsync(IEnumerable<string> tickers)
    {
        return await _dbSet.Where(s => tickers.Contains(s.Ticker))
            .Include(t => t.Trades)
            .AsNoTracking()
            .ToListAsync();
    }
}
