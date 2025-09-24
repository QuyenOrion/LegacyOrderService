using LegacyOrderService.Models;

namespace LegacyOrderService.Tests.UnitTests;

public class OrderEntityTests
{
    [Fact]
    public void ValidInput_InitiatedOrderSuccessfully()
    {
        var order = new Order("James", "Widget", 2, 12.99);

        Assert.Equal("James", order.CustomerName);
        Assert.Equal("Widget", order.ProductName);
        Assert.Equal(2, order.Quantity);
        Assert.Equal(12.99 * 2, order.TotalAmount);
    }

    [Fact]
    public void InvalidInput_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => new Order(null, "Widget", 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("", "Widget", 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order(" ", "Widget", 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("James", null, 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("James", "", 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("James", " ", 2, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("James", "Widget", 0, 12.99));
        Assert.Throws<ArgumentException>(() => new Order("James", "Widget", 2, 0));
        Assert.Throws<ArgumentException>(() => new Order("James", "Widget", 2, -1));
    }
}