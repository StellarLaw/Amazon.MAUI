// AddItemPage.xaml.cs

using Amazon.MAUI.Services;
using Amazon.MAUI.Models;


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

            var newItem = new Item(name, description, price, _inventory.GetNextId(), quantity, isBogo, markdownAmount);
            _inventory.AddItem(newItem);
            _refreshCallback.Invoke();
            
          

            Navigation.PopAsync();
        }
    }
}

