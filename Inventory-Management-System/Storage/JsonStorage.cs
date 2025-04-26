using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using InventoryManagementSystem.Models;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Storage
{
    internal class JsonStorage : IStorage
    {
        private string filePath = "C:/Users/ZBOOK/source/repos/InventoryManagementSystem/InventoryData/inventory.json";

        public List<Product> LoadInventory()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading inventory: " + e.Message);
            }

            return new List<Product>();
        }
        public void SaveInventory(List<Product> products)
        {
            try
            {
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                string json = JsonConvert.SerializeObject(products, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(filePath, json);
                Console.WriteLine("Inventory saved successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error saving inventory: {e.Message}");
            }
        }
    }
}