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
    public partial class AwardedJobs : ContentPage
    {
        MySqlConnection conn;
        public AwardedJobs()
        {
            InitializeComponent();
            var listView = new ListView();
            string ITable = "bidders";
            var items = new ObservableCollection<ListItem> { };
            listView.ItemsSource = items;
            conn = DBUtils.CreateConnection();
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetAllAwardedProducts(conn, ITable);
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
                DisplayAlert("Error", "ERROR: Did not query AWS DB sucessfully!" + "\n" + e, "OK");
            }
            finally
            {
                conn.Close();
            }
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
            Button refreshButton = new Button()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 100,
                Text = "Refresh"

            };
            refreshButton.Clicked += async (sender, args) =>
            {
                try
                {
                    conn.Open();
                    items.Clear();
                    MySqlDataReader rows = DBUtils.GetAllAwardedProducts(conn, ITable);
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
            Button AwardedJobsByDate = new Button()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 100,
                Text = "Search By Date"

            };
            AwardedJobsByDate.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new AwardedJobsDate());
            };
            Button AwardedJobsByDateRange = new Button()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.End,
                HeightRequest = 100,
                WidthRequest = 100,
                Text = "Search By Date Range"

            };
            AwardedJobsByDateRange.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new AwardedJobsByDateRange());
            };
            StackLayout viewLayoutButtons = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 15,
                Children = { refreshButton, AwardedJobsByDate, AwardedJobsByDateRange }
            };
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    listView,
                    viewLayoutButtons
                }
            };
            this.Content = stack;
        }
    }
}