using System;
using Microsoft.Data.Sqlite;
using LegacyOrderService.Models;

namespace LegacyOrderService.Data
{
    public class OrderRepository
    {
        private string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "orders.db")}";


        public void Save(Order order)
        {
            try
            {
                using var connection = new SqliteConnection(_connectionString);

                connection.Open();

                using var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Orders (CustomerName, ProductName, Quantity, Price)
                    VALUES (@customer, @product, @quantity, @price)";

                command.Parameters.AddWithValue("@customer", order.CustomerName);
                command.Parameters.AddWithValue("@product", order.ProductName);
                command.Parameters.AddWithValue("@quantity", order.Quantity);
                command.Parameters.AddWithValue("@price", order.Price);

                var rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    throw new Exception("No rows inserted");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving order: " + ex.Message);
            }
        }
    }
}
