using RoyWeller.LondonStockApi.Exceptions;
using RoyWeller.LondonStockApi.Model;
using RoyWeller.LondonStockApi.Persistence.Repositories.Interfaces;
using RoyWeller.LondonStockApi.Services;

namespace RoyWeller.LondonStockApi.Tests.Translators;

public class TradeValidatorServiceTests
{
    private TradeValidationService _service;
    private Mock<IBrokerRespository> _brokerRepositoryMock;

    [SetUp]
    public void Setup()
    {
        _brokerRepositoryMock = new Mock<IBrokerRespository>();
        _service = new TradeValidationService(_brokerRepositoryMock.Object);
    }

    [Test]
    public async Task ValidateTradeAsync_WhenValid_DoesNotThrowException()
    {
        //Arrange
        _brokerRepositoryMock.Setup(b => b.BrokerExists(It.IsAny<int>())).ReturnsAsync(true);
        var trade = new TradeDto() { Price = 1, Quantity = 1, BrokerId = 1 };

        //Act
        await _service.ValidateTradeAsync(trade);
    }

    [TestCase(0, 1)]
    [TestCase(1, 0)]
    public void ValidateTradeAsync_WhenInValid_ThrowsException(decimal price, int quantity)
    {
        //Arrange
        _brokerRepositoryMock.Setup(b => b.BrokerExists(It.IsAny<int>())).ReturnsAsync(true);
        var trade = new TradeDto() { Price = price, Quantity = quantity, BrokerId = 1 };

        //Act
        Assert.ThrowsAsync<TradeValidationException>(async () => await _service.ValidateTradeAsync(trade));
    }

    [Test]
    public void ValidateTradeAsync_WhenBrokerDoesNotExist_ThrowsException()
    {
        //Arrange
        _brokerRepositoryMock.Setup(b => b.BrokerExists(It.IsAny<int>())).ReturnsAsync(false);
        var trade = new TradeDto() { Price = 1, Quantity = 1, BrokerId = 1 };

        //Act
        Assert.ThrowsAsync<TradeValidationException>(async () => await _service.ValidateTradeAsync(trade));
    }
}