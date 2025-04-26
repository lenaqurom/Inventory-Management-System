using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Management
{
    internal abstract class BaseInventory
    {
        protected List<Product> products = new List<Product>();

        public abstract void AddProduct(string name, decimal price, int quantity);
        public abstract void ViewProducts();
        public abstract void EditProduct(string name);
        public abstract void DeleteProduct(string name);
        public abstract void SearchProduct(string name);
    }
}
