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
    public partial class AwardedJobsDate : ContentPage
    {
        MySqlConnection conn;
        public AwardedJobsDate()
        {
            InitializeComponent();
            this.Title = "Awarded Jobs by Date";
            var listView = new ListView();
            string ITable = "bidders";
            var items = new ObservableCollection<ListItem> { };
            DatePicker datePicker = new DatePicker
            {
                Format = "yyyy-MM-dd"
            };
            listView.ItemsSource = items;
            conn = DBUtils.CreateConnection();
            listView.RowHeight = 85;
            listView.BackgroundColor = Color.White;
            listView.ItemTemplate = new DataTemplate(typeof(AwardedJobsHeaderCell));
            listView.SeparatorColor = Color.Black;
            listView.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Bid: " + item.ID + " was selected." + "\n" +
                    "Bid Status: " + item.BidStatus + "\n" + "Bid Amount: " + item.BidAmt
                    + "\n" + "User: " + item.user_id + "\n" +
                    "Job ID: " + item.jobid, "OK");
                ((ListView)sender).SelectedItem = null;
            };
            Button searchButton = new Button()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 100,
                Text = "Search"

            };
            searchButton.Clicked += async (sender, args) =>
            {
                DateTime date = Convert.ToDateTime(datePicker.Date);
                try
                {
                    conn.Open();
                    items.Clear();
                    MySqlDataReader rows = DBUtils.GetSelectedAwardedProducts(conn, ITable, date.ToString("yyyy-MM-dd"));
                    while (rows.Read())
                    {
                        int BidAmt = (int)rows["BidAmt"];
                        string BidStatus = (string)rows["BidStatus"];
                        int userid = (int)rows["id"];
                        int jobid = (int)rows["jobid"];
                        string user_id = (string)rows["user_id"];
                        var thing = new ListItem()
                        {
                            ID = (int)rows["BiddingID"],
                            jobid = jobid,
                            BidAmt = BidAmt,
                            BidStatus = BidStatus,
                            userid = userid,
                            user_id = user_id
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
            StackLayout viewLayoutButtons = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 15,
                Children = { searchButton }
            };
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    datePicker,
                    listView,
                    viewLayoutButtons
                }
            };
            this.Content = stack;
        }
    }
}