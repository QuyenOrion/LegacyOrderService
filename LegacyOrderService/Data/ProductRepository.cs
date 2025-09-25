using Microsoft.Data.Sqlite;

namespace LegacyOrderService.Data;

public class ProductRepository
{
    private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "products.db")}";

    private readonly Dictionary<string, double> _productPrices = new()
    {
        ["Widget"] = 12.99,
        ["Gadget"] = 15.49,
        ["Doohickey"] = 8.75
    };

    public double GetPrice(string productName)
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = @"
                    SELECT Price
                    FROM Products
                    WHERE Name = @name;";

        command.Parameters.AddWithValue("@name", productName);

        var price = (double?)command.ExecuteScalar();

        return price == null ? throw new Exception("Product not found") : (double)price;
    }

    public void SeedProduct()
    {
        using var connection = new SqliteConnection(_connectionString);

        connection.Open();

        using var command = connection.CreateCommand();

        command.CommandText = "SELECT COUNT(*) FROM Products;";
        var totalRecord = (long?)command.ExecuteScalar();

        if (totalRecord != null && totalRecord.Value == _productPrices.Count)
            return;

        command.CommandText = "DELETE FROM Products;";
        command.ExecuteNonQuery();

        var insertCommand = "INSERT INTO Products (Name, Price) VALUES ";
        foreach (var item in _productPrices)
        {
            insertCommand += $"('{item.Key}', {item.Value}),";
        }

        insertCommand = insertCommand.TrimEnd(',');
        command.CommandText = insertCommand;
        command.ExecuteNonQuery();
    }
}
