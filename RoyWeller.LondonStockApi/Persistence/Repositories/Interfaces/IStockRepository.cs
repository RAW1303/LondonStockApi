using RoyWeller.LondonStockApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
internal interface IStockRepository
{
    Task CreateMissingStocksAsync(IEnumerable<string> tickers);
    Task<IEnumerable<Stock>> GetStocksAsync();
    Task<IEnumerable<Stock>> GetStocksAsync(IEnumerable<string> tickers);
}
