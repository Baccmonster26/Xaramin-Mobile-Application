using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using MySqlConnector;
namespace JBA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventorySearched : ContentPage
    {
        MySqlConnection conn;
        public InventorySearched()
        {
            InitializeComponent();
            this.Title = "Inventory";
            var listView = new ListView();
            string ITable = "inventory";
            string selectedItem = (string)Application.Current.Properties["SelectedCrop"];
            var items = new ObservableCollection<ListItem> { };
            listView.ItemsSource = items;
            var plantList = new List<string>();
            plantList.Add("Red Peppers");
            plantList.Add("Cucumbers");
            plantList.Add("Yellow Squash");
            plantList.Add("Onions");
            plantList.Add("Purple Onion");
            plantList.Add("Potato");
            conn = DBUtils.CreateConnection();
            var picker = new Picker { Title = "Select a plant/crop", TitleColor = Color.Red };
            picker.ItemsSource = plantList;
            listView.RowHeight = 100;
            listView.BackgroundColor = Color.White;
            listView.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView.SeparatorColor = Color.Black;
            listView.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, selectedItem);
                while (rows.Read())
                {
                    var thing = new ListItem()
                    {
                        inventoryID = (int)rows["inventoryID"],
                        location = (string)rows["location"],
                        harvestDate = Convert.ToDateTime(rows["harvestDate"]),
                        harvestDetail = (string)rows["harvestDetail"],
                        itemsStored = (int)rows["itemsStored"],
                        onOrder = (string)rows["onOrder"],
                        deleted = (int)rows["deleted"],
                        harvestID = (int)rows["harvestID"],
                        enteredBy = (string)rows["enteredBy"]
                    };
                    items.Add(thing);
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error", "ERROR: Did not query AWS DB sucessfully!" + "\n" + e, "OK");
            }
            finally
            {
                conn.Close();
            }
            Button refreshButton = new Button()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 100,
                Text = "Refresh"

            };
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Children =
                {
                    listView
                }
            };
            picker.SelectedIndexChanged += (sender, args) =>
            {
                items.Clear();
                if (picker.SelectedIndex == 0)
                {
                    selectedItem = "Red Peppers";
                }
                else if (picker.SelectedIndex == 1)
                {
                    selectedItem = "Cucumbers";
                }
                else if (picker.SelectedIndex == 2)
                {
                    selectedItem = "Yellow Squash";
                }
                else if (picker.SelectedIndex == 3)
                {
                    selectedItem = "Onions";
                }
                else if (picker.SelectedIndex == 4)
                {
                    selectedItem = "Purple Onion";
                }
                else
                {
                    selectedItem = "Potato";
                }
                Application.Current.Properties["SelectedCrop"] = selectedItem;
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, selectedItem);
                    while (rows.Read())
                    {
                        var thing = new ListItem()
                        {
                            inventoryID = (int)rows["inventoryID"],
                            location = (string)rows["location"],
                            harvestDate = Convert.ToDateTime(rows["harvestDate"]),
                            harvestDetail = (string)rows["harvestDetail"],
                            itemsStored = (int)rows["itemsStored"],
                            onOrder = (string)rows["onOrder"],
                            deleted = (int)rows["deleted"],
                            harvestID = (int)rows["harvestID"],
                            enteredBy = (string)rows["enteredBy"]
                        };
                        items.Add(thing);
                    }
                }
                catch (Exception e)
                {
                    DisplayAlert("Error", "ERROR: Did not query AWS DB sucessfully!" + "\n" + e, "OK");
                }
                finally
                {
                    conn.Close();
                }
            };
            refreshButton.Clicked += async (sender, args) =>
            {
                items.Clear();
                try
                {
                    selectedItem = (string)Application.Current.Properties["SelectedCrop"];
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, selectedItem);
                    while (rows.Read())
                    {
                        var thing = new ListItem()
                        {
                            inventoryID = (int)rows["inventoryID"],
                            location = (string)rows["location"],
                            harvestDate = Convert.ToDateTime(rows["harvestDate"]),
                            harvestDetail = (string)rows["harvestDetail"],
                            itemsStored = (int)rows["itemsStored"],
                            onOrder = (string)rows["onOrder"],
                            deleted = (int)rows["deleted"],
                            harvestID = (int)rows["harvestID"],
                            enteredBy = (string)rows["enteredBy"]
                        };
                        items.Add(thing);
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", "ERROR: Did not query AWS DB sucessfully!" + "\n" + e, "OK");
                }
                finally
                {
                    conn.Close();
                }
            };
            StackLayout finalstack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Children =
                {
                    picker,
                    listView,
                    refreshButton
                }
            };
            this.Content = finalstack;
        }
    }
}