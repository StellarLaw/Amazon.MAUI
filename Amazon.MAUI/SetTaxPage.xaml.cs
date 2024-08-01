namespace Amazon.MAUI;

public partial class SetTaxPage : ContentPage
{
    private Shop _shop;

    public SetTaxPage(Shop shop)
    {
        InitializeComponent();
        _shop = shop;
    }

    private void OnSaveClicked(object sender, EventArgs e)
    {
        if (double.TryParse(TaxRateEntry.Text, out double taxRate))
        {
            _shop.SetTaxRate(taxRate);
            DisplayAlert("Success", "Tax rate saved successfully.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Invalid tax rate entered.", "OK");
        }
    }
}