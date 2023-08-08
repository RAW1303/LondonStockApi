namespace RoyWeller.LondonStockApi.Model;
public class TradeDto
{
    public string StockTicker { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int BrokerId { get; set; }
}
