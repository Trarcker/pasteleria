namespace BakeryPOS.Core.Models;

public class SaleDetail
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public decimal Quantity { get; set; }
    public decimal Subtotal => UnitPrice * Quantity;

    public Product? Product { get; set; }
    public Sale? Sale { get; set; }
}