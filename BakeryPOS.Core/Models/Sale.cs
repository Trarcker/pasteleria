namespace BakeryPOS.Core.Models;

public class Sale
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int UserId { get; set; }
    public decimal Total { get; set; }
    public decimal CashPaid { get; set; }
    public decimal ChangeGiven { get; set; }

    public User? User { get; set; }
    public List<SaleDetail> Items { get; set; } = new();
}