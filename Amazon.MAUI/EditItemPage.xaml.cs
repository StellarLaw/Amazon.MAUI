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
            IsBogoEntry.Text = item.IsBogo.ToString();  // Set IsBogo
            MarkdownEntry.Text = item.MarkdownAmount.ToString();
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            var description = DescriptionEntry.Text;
            var price = double.Parse(PriceEntry.Text);
            var quantity = int.Parse(QuantityEntry.Text);
            bool isBogo;
            if (bool.TryParse(IsBogoEntry.Text, out bool result))
            {
                isBogo = result;
            }
            else
            {
                DisplayAlert("Error", "Invalid BOGO value. Please enter 'true' or 'false'.", "OK");
                return;
            }
            var markdownAmount = double.Parse(MarkdownEntry.Text);

            _inventory.UpdateItem(
                _item.Id,
                name,
                description,
                price,
                quantity,
                isBogo,
                markdownAmount);

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

