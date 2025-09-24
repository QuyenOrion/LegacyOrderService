using System;
using LegacyOrderService.Models;
using LegacyOrderService.Data;

namespace LegacyOrderService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Welcome to Order Processor!");

                string name = ReadRequiredInputFromConsole("Customer name");
                string product = ReadRequiredInputFromConsole("Product name");

                var productRepo = new ProductRepository();
                double price = productRepo.GetPrice(product);

                int quantity = ReadIntegerFromConsole("Quantity");

                Console.WriteLine("Processing order...");

                var order = new Order(name, product, quantity, price);

                Console.WriteLine("Order complete!");
                DisplayOrderOnConsole(order);

                Console.WriteLine("Saving order to database...");
                var repo = new OrderRepository();
                repo.Save(order);
                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void DisplayOrderOnConsole(Order order)
        {
            Console.WriteLine("Customer: " + order.CustomerName);
            Console.WriteLine("Product: " + order.ProductName);
            Console.WriteLine("Quantity: " + order.Quantity);
            Console.WriteLine("Total: $" + order.TotalAmount);
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

        private static int ReadIntegerFromConsole(string fieldName)
        {
            int value;
            bool isNotValid = true;
            do
            {
                string input = ReadRequiredInputFromConsole(fieldName);
                if (!int.TryParse(input, out value))
                {
                    Console.WriteLine($"{fieldName} must be an integer.");
                }
                else if (value == 0)
                {
                    Console.WriteLine($"{fieldName} must not be zero.");
                }
                else
                    isNotValid = false;
            } while (isNotValid);

            return value;
        }
    }
}
