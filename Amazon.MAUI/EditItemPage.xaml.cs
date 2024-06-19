// EditItemPage.xaml.cs
using Amazon.MAUI.Services;

namespace Amazon.MAUI
{
    public partial class EditItemPage : ContentPage
    {
        private Inventory _inventory;
        private Item _item;
        private Action _refreshCallback;

        public EditItemPage(Inventory inventory, Item item, Action refreshCallback)
        {
            InitializeComponent();
            _inventory = inventory;
            _item = item;
            _refreshCallback = refreshCallback;
            NameEntry.Text = item.Name;
            DescriptionEntry.Text = item.Description;
            PriceEntry.Text = item.Price.ToString();
            QuantityEntry.Text = item.Quantity.ToString();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            _inventory.UpdateItem(
                _item.Id,
                NameEntry.Text,
                DescriptionEntry.Text,
                double.Parse(PriceEntry.Text),
                int.Parse(QuantityEntry.Text));
            _refreshCallback.Invoke();
            Navigation.PopAsync();
        }

        private void OnDeleteClicked(object sender, EventArgs e)
        {
            _inventory.DeleteItem(_item.Id);
            _refreshCallback.Invoke();
            Navigation.PopAsync();
        }
    }
}

