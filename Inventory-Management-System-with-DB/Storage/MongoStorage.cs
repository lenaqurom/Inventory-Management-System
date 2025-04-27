using MongoDB.Driver;
using InventoryManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace InventoryManagementSystem.Storage
{
    public class MongoStorage : IStorage
    {
        private readonly IMongoCollection<Product> _productsCollection;
        
        public MongoStorage(string connectionString)
        {
            var client = new MongoClient(connectionString); 
            var database = client.GetDatabase("Inventory");
            _productsCollection = database.GetCollection<Product>("Products");    
        }

        public List<Product> LoadInventory()
        {
            try
            {
                return _productsCollection.Find(product => true).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading inventory from MongoDB: " + ex.Message);
                return new List<Product>();
            }
        }

        public void SaveInventory(List<Product> products)
        {
            try
            {
                _productsCollection.DeleteMany(product => true);
                _productsCollection.InsertMany(products);
                Console.WriteLine("Inventory saved successfully to MongoDB.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving inventory to MongoDB: " + ex.Message);
            }
        }
    }
}
