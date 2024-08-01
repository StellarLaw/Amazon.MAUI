using Amazon.MAUI.Models;
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
        private List<WishList> wishLists; 
        private static int wishListCounter = 1;
        private double _taxRate = 0.0; // Default tax rate

        public Shop(Inventory inventory)
        {
            _inventory = inventory;
            wishLists = new List<WishList>(); // Initialize the wishLists list
        }

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
                    _cart.Add(new Item(item.Name, item.Description, item.Price, item.Id, quantity, item.IsBogo, item.MarkdownAmount));
                }
                item.Quantity -= quantity;
            }
        }
        public double GetTaxRate()
        {
            return _taxRate;
        }

        public void Checkout(WishList wishList)
        {
            if (wishList != null)
            {
                wishList.Items.Clear();
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

        public double GetTotalPrice()
        {
            double subtotal = 0.0;

            foreach (var item in _cart)
            {
                double itemPrice = item.Price - item.MarkdownAmount;

                if (item.IsBogo)
                {
                   
                    int paidItems = (item.Quantity / 2) + (item.Quantity % 2);
                    subtotal += itemPrice * paidItems;
                }
                else
                {
                    subtotal += itemPrice * item.Quantity;
                }
            }

            return subtotal;
        }

        
        public void SetTaxRate(double taxRate)
        {
            _taxRate = taxRate;
        }

        public void CreateWishList()
        {
            string wishListName = $"Wishlist {wishListCounter++}";
            wishLists.Add(new WishList { Name = wishListName, Id = wishListCounter });
        }

        public List<WishList> GetWishLists() => wishLists;

        public void AddToWishList(int wishListId, int itemId, int quantity)
        {
            var wishList = wishLists.FirstOrDefault(w => w.Id == wishListId);
            if (wishList != null)
            {
                var item = _inventory.GetItemById(itemId);
                if (item != null && item.Quantity >= quantity)
                {
                    var wishListItem = wishList.Items.FirstOrDefault(i => i.Id == itemId);
                    if (wishListItem != null)
                    {
                        wishListItem.Quantity += quantity;
                    }
                    else
                    {
                        wishList.Items.Add(new Item(item.Name, item.Description, item.Price, item.Id, quantity, item.IsBogo, item.MarkdownAmount));
                    }
                    item.Quantity -= quantity;
                }
            }
        }

        public void RemoveFromWishList(int wishListId, int itemId)
        {
            var wishList = wishLists.FirstOrDefault(w => w.Id == wishListId);
            if (wishList != null)
            {
                var wishListItem = wishList.Items.Find(i => i.Id == itemId);
                if (wishListItem != null)
                {
                    wishList.Items.Remove(wishListItem);
                    var item = _inventory.GetItemById(itemId);
                    if (item != null)
                    {
                        item.Quantity += wishListItem.Quantity;
                    }
                }
            }
        }

       

    }
}


