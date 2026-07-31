namespace BakeryPOS.Core.Models;

public class CashCut
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime OpeningTime { get; set; } = DateTime.Now;
    public DateTime? ClosingTime { get; set; }
    public decimal InitialCash { get; set; }
    public decimal TotalSalesCash { get; set; }
    public decimal TotalSalesCard { get; set; }
    public decimal ActualCash { get; set; }
    public decimal ExpectedCash => InitialCash + TotalSalesCash;
    public decimal Difference => ActualCash - ExpectedCash;
    public bool IsClosed { get; set; } = false;

    public User? User { get; set; }
}