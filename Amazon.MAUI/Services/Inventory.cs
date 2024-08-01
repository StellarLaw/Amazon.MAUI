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
            new Item("Item1", "Description1", 10.0, 1, 10, false, 0.0),
            new Item("Item2", "Description2", 20.0, 2, 5, false, 0.0),
            new Item("Item3", "Description3", 30.0, 3, 2, false, 0.0),
            new Item("Item4", "Description4", 10.0, 1, 10, false, 0.0),
            new Item("Item5", "Description5", 20.0, 2, 5, false, 0.0),
            new Item("Item6", "Description6", 30.0, 3, 2, false, 0.0),
            new Item("Item7", "Description7", 10.0, 1, 10, false, 0.0),
            new Item("Item8", "Description8", 20.0, 2, 5, false, 0.0),
            new Item("Item9", "Description9", 30.0, 3, 2, false, 0.0)
        };
        }

        public Item GetItemById(int id) => items.Find(i => i.Id == id);
        public List<Item> GetAllItems() => items;
        public void AddItem(Item item) => items.Add(item);
        public void UpdateItem(int id, string name, string description, double price, int quantity, bool isBogo, double markDownAmount)
        {
            var item = GetItemById(id);
            if (item != null)
            {
                item.Name = name;
                item.Description = description;
                item.Price = price;
                item.Quantity = quantity;
                item.IsBogo = isBogo;
                item.MarkdownAmount = markDownAmount;
            }
        }
        public void DeleteItem(int id) => items.RemoveAll(i => i.Id == id);
        public int GetNextId() => items.Count > 0 ? items.Max(i => i.Id) + 1 : 1;
    }
}
