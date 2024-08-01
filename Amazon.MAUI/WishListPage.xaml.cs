

using Amazon.MAUI.Models;
using Amazon.MAUI.Services;

namespace Amazon.MAUI;

public partial class WishListPage : ContentPage
{
    private Shop _shop;
    private WishList _wishList;
    private Inventory _inventory;
    private Item? _selectedWishListItem;
    private double _totalWithTax;

    public WishListPage(Shop shop, WishList wishList, Inventory inventory)
    {
        InitializeComponent();
        _shop = shop;
        _wishList = wishList;

        BindingContext = _wishList;
        RefreshPage();
        _inventory = inventory;
        
    }

    private void OnCheckoutClicked(object sender, EventArgs e)
    {
        _shop.Checkout(_wishList);
        RefreshPage();

    }

    private void RefreshPage()
    {
        WishListItemsView.ItemsSource = null;
        WishListItemsView.ItemsSource = _wishList.Items;
        UpdateTotalPrice();


    }

    private void UpdateTotalPrice()
    {
        double subtotal = 0.0;

        foreach (var item in _wishList.Items)
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

       
        _totalWithTax = subtotal * (1 + _shop.GetTaxRate());

        
        TotalPriceLabel.Text = $"Total (with Tax): {_totalWithTax:C}";
    }

    private void OnWishListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        _selectedWishListItem = e.SelectedItem as Item;
        RemoveSelectedItemButton.IsEnabled = _selectedWishListItem != null;
    }

    private void OnRemoveSelectedItemClicked(object sender, EventArgs e)
    {
        if (_selectedWishListItem != null)
        {
            var inventoryItem = _inventory.GetItemById(_selectedWishListItem.Id);

            if (inventoryItem != null)
            {
                inventoryItem.Quantity += _selectedWishListItem.Quantity;
            }
            else
            {
                _inventory.AddItem(_selectedWishListItem);
            }

            _wishList.Items.Remove(_selectedWishListItem);

            RefreshPage();

            _selectedWishListItem = null;
            RemoveSelectedItemButton.IsEnabled = false;
        }
    }
}
