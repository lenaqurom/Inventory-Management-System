using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Models
{
    internal class Product : Item
    {
        public Product(string name, decimal price, int quantity) : base(name, price, quantity) { }
        public override string ToString()
        {
            return $"{Name} - ${Price} - {Quantity} in stock";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(ToString());
        }
    }
}

