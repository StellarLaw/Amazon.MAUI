using Amazon.MAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.MAUI
{
    public class Shop
    {
        private Inventory _inventory;
        private List<Item> _cart = new List<Item>();

        public Shop(Inventory inventory) => _inventory = inventory;

        public List<Item> GetAvailableItems() => _inventory.GetAllItems().Where(i => i.Quantity > 0).ToList();

        public void AddToCart(int id, int quantity)
        {
            var item = _inventory.GetItemById(id);
            if (item != null && item.Quantity >= quantity)
            {
                var cartItem = _cart.FirstOrDefault(i => i.Id == id);
                if (cartItem != null)
                {
                    cartItem.Quantity += quantity;
                }
                else
                {
                    _cart.Add(new Item(item.Name, item.Description, item.Price, item.Id, quantity));
                }
                item.Quantity -= quantity;
            }
        }

        public List<Item> GetCartItems() => _cart;

        public void RemoveFromCart(int id)
        {
            var cartItem = _cart.Find(i => i.Id == id);
            if (cartItem != null)
            {
                _cart.Remove(cartItem);
                var item = _inventory.GetItemById(id);
                if (item != null)
                {
                    item.Quantity += cartItem.Quantity;
                }
            }
        }

        public void Checkout() => _cart.Clear();

        public double GetTotalPrice() => _cart.Sum(i => i.Price * i.Quantity);
    }
}


