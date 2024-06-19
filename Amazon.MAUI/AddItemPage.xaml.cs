// AddItemPage.xaml.cs

using Amazon.MAUI.Services;


namespace Amazon.MAUI
{
    public partial class AddItemPage : ContentPage
    {
        private Inventory _inventory;
        private Action _refreshCallback;

        public AddItemPage(Inventory inventory, Action refreshCallback)
        {
            InitializeComponent(); 
            _inventory = inventory;
            _refreshCallback = refreshCallback;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var newItem = new Item(
                NameEntry.Text,
                DescriptionEntry.Text,
                double.Parse(PriceEntry.Text),
                _inventory.GetNextId(),
                int.Parse(QuantityEntry.Text));

            _inventory.AddItem(newItem);
            _refreshCallback.Invoke();
            Navigation.PopAsync();
        }
    }
}

