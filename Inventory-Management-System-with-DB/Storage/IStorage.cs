using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Storage
{
    public interface IStorage
    {
        List<Product> LoadInventory();
        void SaveInventory(List<Product> products);
    }
}