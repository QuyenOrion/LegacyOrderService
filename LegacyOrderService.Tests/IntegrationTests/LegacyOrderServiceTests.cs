using Microsoft.Data.Sqlite;
using Dapper;

namespace LegacyOrderService.Tests.IntegrationTests;

public class LegacyOrderServiceTests
{
    [Fact]
    public void OrderIsValid_SavedToDatabaseSuccessfully()
    {
        dynamic? currentOrder = GetLatestOrder();

        Program.Main(new string[] { "James", "Widget", "2" });

        dynamic? newOrder = GetLatestOrder();

        Assert.NotNull(newOrder);

        Assert.NotEqual(newOrder.Id, currentOrder?.Id ?? 0);

        Assert.Equal("James", newOrder.CustomerName);
        Assert.Equal("Widget", newOrder.ProductName);
        Assert.Equal(2, newOrder.Quantity);
        Assert.Equal(12.99, newOrder.Price);
    }

    private static dynamic? GetLatestOrder()
    {
        string connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection.QueryFirst<dynamic?>(@"
            SELECT Id, CustomerName, ProductName, Quantity, Price 
            FROM Orders 
            WHERE CustomerName = 'James' AND ProductName = 'Widget'
            ORDER BY Id DESC");
    }
}
