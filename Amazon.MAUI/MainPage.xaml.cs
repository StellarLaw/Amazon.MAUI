// MainPage.xaml.cs
using Amazon.MAUI.Services;

namespace Amazon.MAUI
{
    public partial class MainPage : ContentPage
    {
        private Inventory inventory;
        private Shop shop;

        public MainPage()
        {
            InitializeComponent();
            inventory = new Inventory();
            shop = new Shop(inventory);
        }

        private async void OnManageInventoryClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InventoryPage(inventory));
        }

        private async void OnShopClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShopPage(shop));
        }
    }
}

