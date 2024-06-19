using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Id { get; set; }
        public int Quantity { get; set; }

        public Item(string name, string description, double price, int id, int quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Id = id;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Description: {Description}, Price: {Price:C}, Quantity: {Quantity}";
        }
    }
}

