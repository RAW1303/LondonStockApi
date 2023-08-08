namespace RoyWeller.LondonStockApi.Domain;
public class Trade : DomainEntity
{
    public int Id { get; set; }
    public string StockTicker { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int BrokerId { get; set; }
    public virtual Broker Broker { get; set; }
}
