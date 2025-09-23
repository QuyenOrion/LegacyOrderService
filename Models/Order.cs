namespace LegacyOrderService.Models
{
    public class Order
    {
        public required string CustomerName { get; init; }
        public required string ProductName { get; init; }
        public int Quantity { get; init; }
        public double Price { get; init; }

        public Order(int quantity, double price)
        {
            Quantity = quantity;
            Price = price;
        }

        public double TotalAmount => Quantity * Price;
    }
}
