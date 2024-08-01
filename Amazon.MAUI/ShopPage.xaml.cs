// ShopPage.xaml.cs
using Amazon.MAUI.Models;
using Amazon.MAUI.Services;

namespace Amazon.MAUI
{
    public partial class ShopPage : ContentPage
    {
        private Shop _shop;
        private Inventory _inventory;
        

        private Item? _selectedInventoryItem;
        private Item? _selectedCartItem;
        private WishList? _selectedWishList; 


        public ShopPage(Shop shop, Inventory inventory)
        {
            InitializeComponent();
            _shop = shop;

            _inventory = inventory;
            RefreshData();
            
        }

        private void RefreshData()
        {
            InventoryListView.ItemsSource = null;
            CartListView.ItemsSource = null;
            WishListView.ItemsSource = null;

            InventoryListView.ItemsSource = _shop.GetAvailableItems();
            CartListView.ItemsSource = _shop.GetCartItems();
            WishListView.ItemsSource = _shop.GetWishLists();
            UpdateTotalPrice();
        }

        private void OnInventoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedInventoryItem = e.SelectedItem as Item;
            AddToCartButton.IsEnabled = _selectedInventoryItem != null;
            AddToWishlistButton.IsEnabled = _selectedInventoryItem != null && _selectedWishList != null;
        }

        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            if (_selectedInventoryItem != null)
            {
                _shop.AddToCart(_selectedInventoryItem.Id, 1);
                RefreshData();
                //AddToCartButton.IsEnabled = false;
                //InventoryListView.SelectedItem = null;
            }
        }

        private void OnCartItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedCartItem = e.SelectedItem as Item;
            RemoveFromCartButton.IsEnabled = _selectedCartItem != null;
        }

        private void OnRemoveFromCartClicked(object sender, EventArgs e)
        {
            if (_selectedCartItem != null)
            {
                _shop.RemoveFromCart(_selectedCartItem.Id);
                RefreshData();
                RemoveFromCartButton.IsEnabled = false;
                CartListView.SelectedItem = null;
            }
        }

        private void OnCheckoutClicked(object sender, EventArgs e)
        {
            _shop.Checkout();
            RefreshData();
        }

        private void OnWishListSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _selectedWishList = e.SelectedItem as WishList;
            AddToWishlistButton.IsEnabled = _selectedInventoryItem != null && _selectedWishList != null;
            if (_selectedWishList != null)
            {
                Navigation.PushAsync(new WishListPage(_shop, _selectedWishList, _inventory));
                //DeleteWishListButton.IsEnabled = true;
            }
           

        }

        private void OnCreateWishListClicked(object sender, EventArgs e)
        {
            _shop.CreateWishList();
            RefreshData();
        }

        private void OnAddToWishlistClicked(object sender, EventArgs e)
        {
            if (_selectedInventoryItem != null && _selectedWishList != null)
            {
                _shop.AddToWishList(_selectedWishList.Id, _selectedInventoryItem.Id, 1);
                RefreshData();
                AddToWishlistButton.IsEnabled = false;
                InventoryListView.SelectedItem = null;
            }
        }

        private void UpdateTotalPrice()
        {
            double totalPrice = _shop.GetTotalPrice();
            double _totalWithTax =  totalPrice * (1 + _shop.GetTaxRate());
            TotalPriceLabel.Text = $"Total Price: {_totalWithTax:C}";
        }

       /* private void OnDeleteWishlistClicked(object sender, EventArgs e)
        {
            if (_selectedWishList != null)
            {

                // Remove the selected wishlist from the list
                _wishLists.Remove(_selectedWishList);

                // Refresh the list view
                WishListView.ItemsSource = null;
                WishListView.ItemsSource = _wishLists;

                // Disable the delete button since the wishlist has been deleted
                DeleteWishListButton.IsEnabled = false;

                // Clear the selection
                WishListView.SelectedItem = null;
                _selectedWishList = null;
            }
        } */
    }
}

             