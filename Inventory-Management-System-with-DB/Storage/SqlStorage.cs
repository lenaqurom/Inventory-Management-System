using System;
using System.Collections.Generic;
using InventoryManagementSystem.Models;
using Microsoft.Data.SqlClient;

namespace InventoryManagementSystem.Storage
{
    public class SqlStorage : IStorage
    {
        private readonly string connectionString;

        public SqlStorage(string connectionString)
        {
            this.connectionString = connectionString;
            EnsureDatabaseAndTable();
        }

        private void EnsureDatabaseAndTable()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
                CREATE TABLE Products (
                    Id INT IDENTITY(1,1) PRIMARY KEY,
                    Name NVARCHAR(100) NOT NULL,
                    Price DECIMAL(18,2) NOT NULL,
                    Quantity INT NOT NULL
                )";

                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<Product> LoadInventory()
        {
            List<Product> products = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name, Price, Quantity FROM Products";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader.GetString(0);
                            decimal price = reader.GetDecimal(1);
                            int quantity = reader.GetInt32(2);

                            products.Add(new Product(name, price, quantity));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading inventory from SQL: " + ex.Message);
            }

            return products;
        }

        public void SaveInventory(List<Product> products)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM Products";
                    using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.ExecuteNonQuery();
                    }

                    foreach (var product in products)
                    {
                        string insertQuery = "INSERT INTO Products (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)";
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@Name", product.Name);
                            insertCommand.Parameters.AddWithValue("@Price", product.Price);
                            insertCommand.Parameters.AddWithValue("@Quantity", product.Quantity);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
                Console.WriteLine("Inventory saved successfully to SQL.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving inventory to SQL: " + ex.Message);
            }
        }
    }
}
