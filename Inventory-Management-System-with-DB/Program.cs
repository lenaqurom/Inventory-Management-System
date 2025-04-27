using InventoryManagementSystem.Models;
using InventoryManagementSystem.Management;
using InventoryManagementSystem.Storage;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select the storage type:");
            Console.WriteLine("1. MongoDB");
            Console.WriteLine("2. SQL Server");
            Console.WriteLine("3. JSON File");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            IStorage storage;

            switch (choice)
            {
                case "1":
                    storage = new MongoStorage("mongodb+srv://lina:inventory@inventory.4zomlq5.mongodb.net/?retryWrites=true&w=majority&appName=Inventory");
                    break;
                case "2":
                    storage = new SqlStorage("Server=localhost;Database=InventoryDB;Trusted_Connection=True;TrustServerCertificate=True;");
                    break;
                case "3":
                    storage = new JsonStorage();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Exiting...");
                    return;
            }

            Inventory inventory = new Inventory(storage);


            while (true)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. View Products");
                Console.WriteLine("3. Edit Product");
                Console.WriteLine("4. Delete Product");
                Console.WriteLine("5. Search Product");
                Console.WriteLine("6. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter product price: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal price))
                        {
                            Console.WriteLine("Invalid price! Please enter a valid number.");
                            break;
                        }
                        Console.Write("Enter product quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Invalid quantity! Please enter a valid number.");
                            break;
                        }
                        inventory.AddProduct(name, price, quantity);
                        break;

                    case "2":
                        inventory.ViewProducts();
                        break;

                    case "3":
                        Console.Write("Enter the name of the product to edit: ");
                        inventory.EditProduct(Console.ReadLine());
                        break;

                    case "4":
                        Console.Write("Enter the name of the product to delete: ");
                        inventory.DeleteProduct(Console.ReadLine());
                        break;

                    case "5":
                        Console.Write("Enter the name of the product to search: ");
                        inventory.SearchProduct(Console.ReadLine());
                        break;

                    case "6":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }
    }
}