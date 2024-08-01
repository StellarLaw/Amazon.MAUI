// InventoryPage.xaml.cs
using Amazon.MAUI.Services;
namespace Amazon.MAUI
{
    public partial class InventoryPage : ContentPage
    {
        public Inventory _inventory;

        public InventoryPage(Inventory inventory)
        {
            InitializeComponent();
            _inventory = inventory;
            RefreshPageData();
        }

        public void RefreshPageData()
        {
            ItemsListView.ItemsSource = null;
            ItemsListView.ItemsSource = _inventory.GetAllItems();
        }

        private async void OnAddItemClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddItemPage(_inventory, RefreshPageData));
            RefreshPageData ();
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var item = (Item)e.SelectedItem;
                await Navigation.PushAsync(new EditItemPage(_inventory, item, RefreshPageData));
            }
        }
    }
}


