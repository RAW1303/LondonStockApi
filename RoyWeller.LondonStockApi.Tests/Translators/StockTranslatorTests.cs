using RoyWeller.LondonStockApi.Domain;
using RoyWeller.LondonStockApi.Translators;

namespace RoyWeller.LondonStockApi.Tests.Translators;

public class StockTranslatorTests
{
    private Fixture _fixture;

    public StockTranslatorTests()
    {
        _fixture = new Fixture();
    }

    [TestCase(new double[] { 1.0, 5.0, 12.0 }, 6.0)]
    [TestCase(new double[0], 0)]
    public void TranslateToDto_WhenSingleStock_ReturnsCorrectStockPrice(double[] prices, double expectedPrice)
    {
        //Arrange
        var decimalPrices = prices.Select(x => new Decimal(x));
        var trades = prices
            .Select(x => _fixture
                .Build<Trade>()
                .With(t => t.Price, new decimal(x))
                .Create());

        var stock = _fixture.Build<Stock>().With(s => s.Trades, trades).Create();

        //Act
        var result = StockTranslator.TranslateToDto(stock);

        //Assert
        result.Price.Should().Be(new Decimal(expectedPrice));
    }

    [Test, AutoData]
    public void TranslateToDto_WhenListOfStocks_ReturnsCorrectCount(Stock[] stocks)
    {
        //Act
        var result = StockTranslator.TranslateToDto(stocks);

        //Assert
        result.Should().HaveCount(stocks.Length);
    }

    [Test, AutoData]
    public void TranslateToDto_WhenEmptyListOfStocks_ReturnsEmptyList()
    {
        //Act
        var result = StockTranslator.TranslateToDto(new Stock[0]);

        //Assert
        result.Should().BeEmpty();
    }
}