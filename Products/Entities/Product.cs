namespace ProductsFastEndpointsDemo.Products.Entities;

public class Product
{

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public bool IsAvailable { get; set; }

    public Product()
    {

    }

    public Product(string name, decimal price, int quantity, bool isAvailable)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
        IsAvailable = isAvailable;
    }
}