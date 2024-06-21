using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI.Services
{

    public class Inventory
    {
        private List<Item> items;

        public Inventory()
        {
            items = new List<Item>
        {
            new Item("Test 1",  "Description for Test ", 10.0, 1, 5),
            new Item("Test 2",  "Description for Test ", 10.0, 2, 5),
            new Item("Test 3",  "Description for Test ", 10.0, 3, 5),
        };
        }

        public Item GetItemById(int id) => items.Find(i => i.Id == id);
        public List<Item> GetAllItems() => items;
        public void AddItem(Item item) => items.Add(item);
        public void UpdateItem(int id, string name, string description, double price, int quantity)
        {
            var item = GetItemById(id);
            if (item != null)
            {
                item.Name = name;
                item.Description = description;
                item.Price = price;
                item.Quantity = quantity;
            }
        }
        public void DeleteItem(int id) => items.RemoveAll(i => i.Id == id);
        public int GetNextId() => items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
    }
}
