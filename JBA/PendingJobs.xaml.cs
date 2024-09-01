﻿using System;
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
    public partial class PendingJobs : ContentPage
    {
        MySqlConnection conn;
        public PendingJobs()
        {
            InitializeComponent();
            var listView = new ListView();
            string ITable = "jobs";
            var items = new ObservableCollection<ListItem> { };
            listView.ItemsSource = items;
            conn = DBUtils.CreateConnection();
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetAllPendingProducts(conn, ITable);
                while (rows.Read())
                {
                    int jobgroupid = (int)rows["jobgroupid"];
                    int BidAmt = (int)rows["BidAmt"];
                    string BidStatus = (string)rows["BidStatus"];
                    string jobpostdescription = (string)rows["jobpostingdescription"];
                    string jobstatus = (string)rows["jobstatus"];
                    int biddingid = (int)rows["biddingid"];
                    string userprofileid = (string)rows["userprofileid"];
                    var thing = new ListItem()
                    {
                        ID = (int)rows["jobid"],
                        BidAmt = BidAmt,
                        BidStatus2 = BidStatus,
                        jobgroupid = jobgroupid,
                        jobpostingdescription = jobpostdescription,
                        jobstatus = jobstatus,
                        biddingid = biddingid,
                        userprofileid = userprofileid
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
            listView.ItemTemplate = new DataTemplate(typeof(PendingJobsHeaderCell));
            listView.SeparatorColor = Color.Black;
            listView.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Job " + item.ID + " was selected." + "\n" +
                    "Job Status: " + item.jobstatus + "\n" + "Bidding ID: " + item.biddingid
                    + "\n" + "Job Description: " + item.jobpostingdescription + 
                    "\n" + "Bidding Amount: " + item.BidAmt + "\n" + 
                    "Bidding Status: " + item.BidStatus2, "OK");
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
                    MySqlDataReader rows = DBUtils.GetAllPendingProducts(conn, ITable);
                    while (rows.Read())
                    {
                        int jobgroupid = (int)rows["jobgroupid"];
                        int BidAmt = (int)rows["BidAmt"];
                        string BidStatus = (string)rows["BidStatus"];
                        string jobpostdescription = (string)rows["jobpostingdescription"];
                        string jobstatus = (string)rows["jobstatus"];
                        int biddingid = (int)rows["biddingid"];
                        string userprofileid = (string)rows["userprofileid"];
                        var thing = new ListItem()
                        {
                            ID = (int)rows["jobid"],
                            BidAmt = BidAmt,
                            BidStatus2 = BidStatus,
                            jobgroupid = jobgroupid,
                            jobpostingdescription = jobpostdescription,
                            jobstatus = jobstatus,
                            biddingid = biddingid,
                            userprofileid = userprofileid
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
            StackLayout stack = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    listView,
                    refreshButton
                }
            };
            this.Content = stack;
        }
    }
}