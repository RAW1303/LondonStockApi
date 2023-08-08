using RoyWeller.LondonStockApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services.Interfaces;
public interface IStockService
{
    Task<IEnumerable<StockDto>> GetStocksAsync();
    Task<IEnumerable<StockDto>> GetStocksAsync(IEnumerable<string> tickers);
}
