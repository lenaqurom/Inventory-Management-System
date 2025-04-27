using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Storage;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem.Management
{
    public class Inventory : BaseInventory
    {
        private readonly List<IStorage> storages = new List<IStorage>();

        public Inventory(params IStorage[] storages)
        {
            if(storages.Length > 0)
            {
                products = storages[0].LoadInventory();
            }
            this.storages.AddRange(storages);
        }

        private void SaveAll()
        {
            foreach(var storage in storages)
            {
                storage.SaveInventory(products);
            }
        }

        public override void AddProduct(string name, decimal price, int quantity)
        {
            products.Add(new Product(name, price, quantity));
            SaveAll();
            Console.WriteLine("Product added successfully!");
        }

        public override void ViewProducts()
        {
            if (products.Count == 0)
            {
                Console.WriteLine("Inventory is empty.");
                return;
            }
            else
            {
                Console.WriteLine("\nCurrent Inventory:");
                foreach (var product in products)
                {
                    Console.WriteLine(product);
                }
            }
        }
        public override void EditProduct(string name)
        {
            var product = products.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            Console.Write("Enter new name (or press Enter to keep current): ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) product.Name = newName;

            Console.Write("Enter new price (or press Enter to keep current): ");
            string priceInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice)) product.Price = newPrice;

            Console.Write("Enter new quantity (or press Enter to keep current): ");
            string quantityInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(quantityInput) && int.TryParse(quantityInput, out int newQuantity)) product.Quantity = newQuantity;

            SaveAll();
            Console.WriteLine("Product updated successfully!");
        }

        public override void DeleteProduct(string name)
        {
            var product = products.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }

            products.Remove(product);
            SaveAll();
            Console.WriteLine("Product deleted successfully!");
        }

        public override void SearchProduct(string name)
        {
            var product = products.FirstOrDefault(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                Console.WriteLine("Product not found.");
                return;
            }
            else
            {
                Console.WriteLine(product);
            }
        }
    }
}
