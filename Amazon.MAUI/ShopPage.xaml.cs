// ShopPage.xaml.cs
namespace Amazon.MAUI
{
    public partial class ShopPage : ContentPage
    {
        private Shop _shop;
        private Item? _selectedInventoryItem;
        private Item? _selectedCartItem;

        public ShopPage(Shop shop)
        {
            InitializeComponent();
            _shop = shop;
            RefreshPageData();
        }

        private void RefreshPageData()
        {
            InventoryListView.ItemsSource = null;
            CartListView.ItemsSource = null;

            InventoryListView.ItemsSource = _shop.GetAvailableItems();
            CartListView.ItemsSource = _shop.GetCartItems();

            UpdateTotalPrice();
        }

        private void OnInventoryItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                _selectedInventoryItem = (Item)e.SelectedItem;
                AddToCartButton.IsEnabled = true;
            }
        }

        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            if (_selectedInventoryItem != null)
            {
                _shop.AddToCart(_selectedInventoryItem.Id, 1); // Add 1 item to the cart
                RefreshPageData();
                AddToCartButton.IsEnabled = false; // Disable add button after adding
                InventoryListView.SelectedItem = null;
                _selectedInventoryItem = null;
                Console.WriteLine("Item Added");
            }
        }

        private void OnCartItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                _selectedCartItem = (Item)e.SelectedItem;
                RemoveFromCartButton.IsEnabled = true;
                Console.WriteLine("Item selected" + _selectedCartItem.Name);

            }
            else
            {
                RemoveFromCartButton.IsEnabled = false;
            }
        }

        private void OnRemoveFromCartClicked(object sender, EventArgs e)
        {
            if (_selectedCartItem != null)
            {
                _shop.RemoveFromCart(_selectedCartItem.Id);
                RefreshPageData();
                RemoveFromCartButton.IsEnabled = false;
                CartListView.SelectedItem = null;
                _selectedCartItem = null; // Clear the selected cart item
            }
        }

        private void UpdateTotalPrice()
        {
            TotalPriceLabel.Text = $"Total Price: {_shop.GetTotalPrice():C}";
        }

        private void OnCheckoutClicked(object sender, EventArgs e)
        {
            _shop.Checkout();
            RefreshPageData();
        }
    }
}



