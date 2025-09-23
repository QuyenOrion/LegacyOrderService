using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Order Processor!");
            
            string name = ReadRequiredInputFromConsole("Customer name");
            string product = ReadRequiredInputFromConsole("Product name");

            var productRepo = new ProductRepository();
            double price = productRepo.GetPrice(product);

            Console.WriteLine("Enter quantity:");
            int qty = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Processing order...");

            var order = new Order(qty, price) { CustomerName = name, ProductName = product };

            Console.WriteLine("Order complete!");
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + order.TotalAmount);

            Console.WriteLine("Saving order to database...");
            var repo = new OrderRepository();
            repo.Save(order);
            Console.WriteLine("Done.");
        }

        private static string ReadRequiredInputFromConsole(string fieldName)
        {
            Console.WriteLine($"Enter {fieldName}:");
            string value = string.Empty;
            do
            {
                string? input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{fieldName} is required. Enter {fieldName} (type 'exit' then press Enter to finish):");
                }
                else if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);
                else
                    value = input;
            } while (string.IsNullOrWhiteSpace(value));

            return value;
        }
    }
}
