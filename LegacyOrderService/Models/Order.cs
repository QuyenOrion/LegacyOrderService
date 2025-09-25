namespace LegacyOrderService.Models;

public class Order
{
    public string CustomerName { get; init; }
    public string ProductName { get; init; }
    public int Quantity { get; init; }
    public double Price { get; init; }

    public Order(string customerName, string productName, int quantity, double price)
    {
        if (string.IsNullOrWhiteSpace(customerName))
            throw new ArgumentException("Customer name must not be null or white space");

        if (string.IsNullOrWhiteSpace(productName))
            throw new ArgumentException("Product name must not be null or white space");

        if (price <= 0)
            throw new ArgumentException("Price must be a positive number");

        if (quantity == 0)
            throw new ArgumentException("Quantity must not be zero");

        Quantity = quantity;
        Price = price;
        CustomerName = customerName;
        ProductName = productName;
    }

    public double TotalAmount => Quantity * Price;
}
