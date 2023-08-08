using RoyWeller.LondonStockApi.Exceptions;
using RoyWeller.LondonStockApi.Model;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using RoyWeller.LondonStockApi.Services.Interfaces;
using System.Threading.Tasks;

namespace RoyWeller.LondonStockApi.Services;
internal class TradeValidationService : ITradeValidationService
{
    private readonly IBrokerRespository _brokerRepository;

    public TradeValidationService(IBrokerRespository brokerRepository)
    {
        _brokerRepository = brokerRepository;
    }

    public async Task ValidateTradeAsync(TradeDto trade)
    {
        if (trade.Price <= 0)
            throw new TradeValidationException("Price must be greater than 0");

        if(trade.Quantity <= 0)
            throw new TradeValidationException("Quantity must be greater than 0");

        if(!await _brokerRepository.BrokerExists(trade.BrokerId))
            throw new TradeValidationException($"No broker with id {trade.BrokerId} exists");
    }
}
