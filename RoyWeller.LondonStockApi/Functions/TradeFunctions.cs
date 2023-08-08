using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RoyWeller.LondonStockApi.Model;
using RoyWeller.LondonStockApi.Services.Interfaces;
using RoyWeller.LondonStockApi.Exceptions;
using System.Web.Http;

namespace RoyWeller.LondonStockApi.Functions;

[ServiceBusAccount("ServiceBusConnectionString")]
public class TradeFunctions
{
    private readonly ILogger _logger;
    private readonly ITradeValidationService _tradeValidationService;
    private readonly ITradeService _tradeService;

    public TradeFunctions(
        ITradeService tradeService,
        ITradeValidationService tradeValidationService,
        ILogger<TradeFunctions> logger) 
    {
        _tradeService = tradeService;
        _tradeValidationService = tradeValidationService;
        _logger = logger;
    }

    [FunctionName(nameof(CreateTrade))]
    public async Task<IActionResult> CreateTrade(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "trades")] HttpRequest req)
    {
        try
        {
            var requestContentString = await req.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TradeDto>(requestContentString);
            await _tradeValidationService.ValidateTradeAsync(data);
            await _tradeService.CreateTradesAsync(new[] { data });
            return new OkResult();
        }
        catch (TradeValidationException ex)
        {
            _logger.LogWarning($"{nameof(AcceptTrade)} failed validation: {ex.Message}");
            return new BadRequestObjectResult(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    [FunctionName(nameof(AcceptTrade))]
    public async Task<IActionResult> AcceptTrade(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "trades/highfrequency")] HttpRequest req,
        [ServiceBus("%TradesQueueName%")]IAsyncCollector<TradeDto> tradeCollector)
    {
        try
        {
            var requestContentString = await req.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<TradeDto>(requestContentString);
            await _tradeValidationService.ValidateTradeAsync(data);
            await tradeCollector.AddAsync(data);
            return new AcceptedResult();
        }
        catch (TradeValidationException ex)
        {
            _logger.LogWarning($"{nameof(AcceptTrade)} failed validation: {ex.Message}");
            return new BadRequestObjectResult(ex.Message);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return new InternalServerErrorResult();
        }
    }

    [FunctionName(nameof(ProcessTradesQueue))]
    public async Task ProcessTradesQueue(
        [ServiceBusTrigger("%TradesQueueName%")] TradeDto[] trades)
    {
        try
        {
            await _tradeService.CreateTradesAsync(trades);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}

