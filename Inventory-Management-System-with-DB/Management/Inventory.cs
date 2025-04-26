using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using InventoryManagementSystem.Models;
using InventoryManagementSystem.Storage;

namespace InventoryManagementSystem.Management
{
    internal class Inventory : BaseInventory
    {
        private readonly IStorage storage;

        public Inventory(IStorage storage)
        {
            this.storage = storage;
            products = storage.LoadInventory();
        }
        public override void AddProduct(string name, decimal price, int quantity)
        {
            products.Add(new Product(name, price, quantity));
            storage.SaveInventory(products);
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

            storage.SaveInventory(products);
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
            storage.SaveInventory(products);
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
