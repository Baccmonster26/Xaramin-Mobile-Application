using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace JBA 
{
    class InventoryHeaderCell : ViewCell
    {
        public InventoryHeaderCell()
        {
            Label InventoryID_Title = new Label();
            InventoryID_Title.Text = "Inventory ID: ";
            InventoryID_Title.TextColor = Color.Black;
            InventoryID_Title.FontAttributes = FontAttributes.Bold;
            InventoryID_Title.FontSize = 18;
            Label harvestDate_Title = new Label();
            harvestDate_Title.Text = "Crop Name: ";
            harvestDate_Title.TextColor = Color.Black;
            harvestDate_Title.FontAttributes = FontAttributes.Bold;
            harvestDate_Title.FontSize = 18;
            Label itemsStored_Title = new Label();
            itemsStored_Title.Text = "Items Stored: ";
            itemsStored_Title.TextColor = Color.Black;
            itemsStored_Title.FontAttributes = FontAttributes.Bold;
            itemsStored_Title.FontSize = 18;
            Label inventoryID = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            inventoryID.SetBinding(Label.TextProperty, "inventoryID");
            Label harvestDetail = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            harvestDetail.SetBinding(Label.TextProperty, "harvestDetail");
            Label itemsStored = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            itemsStored.SetBinding(Label.TextProperty, "itemsStored");
            StackLayout viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { InventoryID_Title, inventoryID }
            };
            StackLayout viewLayout2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { harvestDate_Title, harvestDetail },
            };
            StackLayout viewLayout3 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { itemsStored_Title, itemsStored }
            };
            StackLayout viewLayout4 = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children = { viewLayout, viewLayout2, viewLayout3 }
            };
            View = viewLayout4;
        }
    }
}
