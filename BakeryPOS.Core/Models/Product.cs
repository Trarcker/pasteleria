namespace BakeryPOS.Core.Models;

public enum UnitType
{
    Unit,     // Por pieza
    Kilogram  // Por peso
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public UnitType SalesUnit { get; set; }
    public decimal StockQuantity { get; set; }
    public string? ImagePath { get; set; }
    public bool IsActive { get; set; } = true;
}