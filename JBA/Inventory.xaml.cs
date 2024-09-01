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
    public partial class Inventory : ContentPage
    {
        MySqlConnection conn;
        public Inventory()
        {
            InitializeComponent();
            this.Title = "Inventory";
            var listView = new ListView();
            var listView2 = new ListView();
            var listView3 = new ListView();
            var listView4 = new ListView();
            var listView5 = new ListView();
            var listView6 = new ListView();
            string ITable = "inventory";
            string selectedItem = "";
            var items = new ObservableCollection<ListItem> { };
            var Cucumbers = new ObservableCollection<ListItem> { };
            var YellowSquash = new ObservableCollection<ListItem> { };
            var Onions = new ObservableCollection<ListItem> { };
            var PurpleOnion = new ObservableCollection<ListItem> { };
            var Potato = new ObservableCollection<ListItem> { };
            Label RedPepper_Title = new Label();
            RedPepper_Title.Text = "Red Pepper";
            RedPepper_Title.TextColor = Color.Red;
            RedPepper_Title.FontAttributes = FontAttributes.Bold;
            RedPepper_Title.FontSize = 25;
            Label Cucumbers_Title = new Label();
            Cucumbers_Title.Text = "Cucumbers";
            Cucumbers_Title.TextColor = Color.Red;
            Cucumbers_Title.FontAttributes = FontAttributes.Bold;
            Cucumbers_Title.FontSize = 25;
            Label YellowSquash_Title = new Label();
            YellowSquash_Title.Text = "Yellow Squash";
            YellowSquash_Title.TextColor = Color.Red;
            YellowSquash_Title.FontAttributes = FontAttributes.Bold;
            YellowSquash_Title.FontSize = 25;
            Label Onions_Title = new Label();
            Onions_Title.Text = "Onions";
            Onions_Title.TextColor = Color.Red;
            Onions_Title.FontAttributes = FontAttributes.Bold;
            Onions_Title.FontSize = 25;
            Label PurpleOnion_Title = new Label();
            PurpleOnion_Title.Text = "Purple Onion";
            PurpleOnion_Title.TextColor = Color.Red;
            PurpleOnion_Title.FontAttributes = FontAttributes.Bold;
            PurpleOnion_Title.FontSize = 25;
            Label Potato_Title = new Label();
            Potato_Title.Text = "Potato";
            Potato_Title.TextColor = Color.Red;
            Potato_Title.FontAttributes = FontAttributes.Bold;
            Potato_Title.FontSize = 25;

            BoxView fieldSpacer1 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            BoxView fieldSpacer2 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            BoxView fieldSpacer3 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            BoxView fieldSpacer4 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            BoxView fieldSpacer5 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            BoxView fieldSpacer6 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };
            listView.ItemsSource = items;
            //listView.Header = "Red Peppers";
            listView2.ItemsSource = Cucumbers;
            //listView2.Header = "Cucumbers";
            listView3.ItemsSource = YellowSquash;
            //listView3.Header = "Yellow Squash";
            listView4.ItemsSource = Onions;
            //listView4.Header = "Onions";
            listView5.ItemsSource = PurpleOnion;
            //listView5.Header = "Purple Onion";
            listView6.ItemsSource = Potato;
            //listView6.Header = "Potato";
            var plantList = new List<string>();
            plantList.Add("Red Peppers");
            plantList.Add("Cucumbers");
            plantList.Add("Yellow Squash");
            plantList.Add("Onions");
            plantList.Add("Purple Onion");
            plantList.Add("Potato");

            conn = DBUtils.CreateConnection();
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Red Peppers");
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
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Cucumbers");
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
                    Cucumbers.Add(thing);
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
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Yellow Squash");
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
                    YellowSquash.Add(thing);
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
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Onions");
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
                    Onions.Add(thing);
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
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Purple Onion");
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
                    PurpleOnion.Add(thing);
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
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Potato");
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
                    Potato.Add(thing);
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
            listView2.RowHeight = 100;
            listView2.BackgroundColor = Color.White;
            listView2.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView2.SeparatorColor = Color.Black;
            listView2.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            listView3.RowHeight = 100;
            listView3.BackgroundColor = Color.White;
            listView3.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView3.SeparatorColor = Color.Black;
            listView3.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            listView4.RowHeight = 100;
            listView4.BackgroundColor = Color.White;
            listView4.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView4.SeparatorColor = Color.Black;
            listView4.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            listView5.RowHeight = 100;
            listView5.BackgroundColor = Color.White;
            listView5.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView5.SeparatorColor = Color.Black;
            listView5.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            listView6.RowHeight = 100;
            listView6.BackgroundColor = Color.White;
            listView6.ItemTemplate = new DataTemplate(typeof(InventoryHeaderCell));
            listView6.SeparatorColor = Color.Black;
            listView6.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Inventory ID " + item.inventoryID + " was selected." +
                    "\nLocation: " + item.location + "\nHarvest Date: " + item.harvestDate +
                    "\nCrop/Plant: " + item.harvestDetail + "\nItems Stored: " + item.itemsStored +
                    "\nOn Order: " + item.onOrder + "\nDeleted: " + item.deleted +
                    "\nHarvest ID: " + item.harvestID + "\nEntered By: " + item.enteredBy, "OK");
                ((ListView)sender).SelectedItem = null;
            };
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
                    RedPepper_Title,
                    listView,
                    fieldSpacer1,
                    Cucumbers_Title,
                    listView2,
                    fieldSpacer2,
                    YellowSquash_Title,
                    listView3,
                    fieldSpacer3,
                    Onions_Title,
                    listView4,
                    fieldSpacer4,
                    PurpleOnion_Title,
                    listView5,
                    fieldSpacer5,
                    Potato_Title,
                    listView6,
                    fieldSpacer6
                }
            };
            picker.SelectedIndexChanged += (sender, args) =>
            {
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
                Navigation.PushAsync(new InventorySearched());
            };
            refreshButton.Clicked += async (sender, args) =>
            {
                items.Clear();
                Cucumbers.Clear();
                YellowSquash.Clear();
                Onions.Clear();
                PurpleOnion.Clear();
                Potato.Clear();
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Red Peppers");
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
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Cucumbers");
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
                        Cucumbers.Add(thing);
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
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Yellow Squash");
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
                        YellowSquash.Add(thing);
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
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Onions");
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
                        Onions.Add(thing);
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
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Purple Onion");
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
                        PurpleOnion.Add(thing);
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
                try
                {
                    conn.Open();
                    MySqlDataReader rows = DBUtils.GetSelectedPlant(conn, ITable, "Potato");
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
                        Potato.Add(thing);
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
            ScrollView scrollView = new ScrollView
            {
                Content = stack
            };
            StackLayout finalstack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 0,
                Children =
                {
                    picker,
                    scrollView,
                    refreshButton
                }
            };
            this.Content = finalstack;
        }
    }
}