using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories;
internal class TradeRepository : BaseRespository<Trade>, ITradeRepository
{
    public TradeRepository(DatabaseContext databaseContext) : base(databaseContext) { }

    public async Task CreateTradesAsync(IEnumerable<Trade> trades)
    {
        await _dbSet.AddRangeAsync(trades);
        await _databaseContext.SaveChangesAsync();
    }
}
