//using Java.Sql;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace JBA
{
    public class wonJobsPage : ContentPage
    {
        public class ListItem
        {
            public string jobStatus { get; set; }
            public string jobDescript { get; set; }
            public int jobID { get; set; }
            public string jobDescription { get; set; }
            public int minRate { get; set; }
            public int maxRate { get; set; }
            public int finalRate { get; set; }
            public int BidID { get; set; }

            public DateTime jobDue { get; set; }

            public int BidAmt { get; set; }
            public string BidStatus { get; set; }
        }
        public Entry startDate { get; set; }

        public string startdate { get; set; }
        public string dateCheck { get; set; }
        public string biddingIDCheck { get; set; }
        public DateTime jobDue { get; set; }

        public wonJobsPage()
        {
            Title = "Won Bids";

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//====      Search fields/buttons/labels     ======================================================================
            Image headerImage = new Image
            {
                Source = "greenhouse_update.jpg",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 100,
            };

            Label biddingIDLabel = new Label()
            {
                Text = "Search by Bidding ID: ",
                FontSize = 20,
                TextColor = Color.Black,
            };

            Entry biddingIDEntry = new Entry
            {
                Placeholder = "Enter bidding ID",
                Keyboard = Keyboard.Numeric,
            };

            Label sperate = new Label
            {
                Text = "OR",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
            };

            BoxView searchSpacer = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };

            Label wonJobsLabel = new Label
            {
                Text = "All Won Bids: ",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,

            };

            Button searchButton = new Button()
            {
                Text = "Search",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(90, 25, 90, 25),
                BorderWidth = 2,
                BorderColor = Color.Black,
                BackgroundColor = Color.FromRgb(164, 165, 166),
                CornerRadius = 5,
            };

            
            BoxView searchSpacer1 = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,
            };

            //=================================================================================================================
            //=================================================================================================================
            //=================================================================================================================
            //====     Page load handler to get all of the completed (accepted) jobs      =====================================
            var listView = new ListView();
            var wonJobs = new ObservableCollection<ListItem> { };
            listView.ItemsSource = wonJobs;
            listView.RowHeight = 60;     //60
            listView.SeparatorColor = Color.Black;
            listView.SetValue(TextCell.TextColorProperty, Color.Black);
            listView.SetValue(TextCell.DetailColorProperty, Color.Black);
            MySqlConnection con;
            con = DBUtils.CreateConnection();
            try
            {
                con.Open();
                MySqlDataReader rows = DBUtils.GetWonBids(con, "bidders");
                string userName = Application.Current.Properties["LoggedIn"].ToString();
                string status = "pending";
                DateTime jobdue;
                while (rows.Read())
                {
                    string jobstatus = (string)rows["jobstatus"];
                    string jobdescription = (string)rows["jobpostingdescription"];
                    int jobid = (int)rows["jobid"];
                    int minrate = (int)(float)rows["minrate"];
                    int maxrate = (int)(float)rows["maxrate"];
                    int finalrate = (int)(float)rows["finalrate"];
                    jobdue = Convert.ToDateTime(rows["jobdueby"]);
                    if (userName == (string)rows["userprofileid"] && status == (string)rows["jobstatus"])
                    {
                        var job = new ListItem()
                        {
                            jobStatus = jobstatus,
                            jobID = jobid,
                            jobDescription = jobdescription,
                            minRate = minrate,
                            maxRate = maxrate,
                            finalRate = finalrate,
                            jobDue = jobdue,
                            BidAmt = (int)rows["BidAmt"],
                            BidStatus = (string)rows["BidStatus"],
                            BidID = (int)rows["BiddingID"]
                        };
                        wonJobs.Add(job);
                    }
                };
            }
            catch (Exception e)
            {
                DisplayAlert("ERROR :(  ", "Something Broke! " + e, "OK");
            }
            finally
            {
                con.Close();
            };

            //binds the data
            listView.ItemTemplate = new DataTemplate(typeof(TextCell));
            listView.SetValue(TextCell.TextColorProperty, Color.Black);
            listView.SetValue(TextCell.DetailColorProperty, Color.Black);
            listView.ItemTemplate.SetBinding(TextCell.TextProperty, "BidStatus");
            listView.ItemTemplate.SetBinding(TextCell.DetailProperty, "jobDescription");

            listView.ItemTapped += async (sender, e) =>
            {
                ListItem job = (ListItem)e.Item;
                await DisplayAlert("Job Info", "Job ID: " + job.jobID + "\nDue Date: " + job.jobDue + "\nJob Status: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate + "\nBid Amount: " + job.BidAmt + "\nBid Status: " + job.BidStatus + "\nBidding ID: " + job.BidID, "OK");
                ((ListView)sender).SelectedItem = null;
            };

            var searchedJobsList = new ListView();
            var searchedJob = new ObservableCollection<ListItem> { };
            searchedJobsList.ItemsSource = searchedJob;
            searchedJobsList.RowHeight = 60;
            searchedJobsList.SeparatorColor = Color.Black;
            searchedJobsList.SetValue(TextCell.TextColorProperty, Color.Black);
            searchedJobsList.SetValue(TextCell.DetailColorProperty, Color.Black);

//===============================================================================================================================
//===============================================================================================================================
//=====  Search button clicked handler ==========================================================================================     
            searchButton.Clicked += async (sender, Args) =>
            {
                string idCheck = "bad";

                if (string.IsNullOrWhiteSpace(biddingIDEntry.Text))
                {
                    await DisplayAlert("error", "Please enter a Bidding ID!", "OK");     
                }
                else
                {
                    idCheck = "good";
                }
                con = DBUtils.CreateConnection();
                try
                {

                    con = DBUtils.CreateConnection();
                    con.Open();

                    MySqlDataReader rows = DBUtils.GetWonBids(con, "bidders");
                    while (rows.Read())
                    {
                        if (idCheck == "good")
                        {
                            string userName = (string)rows["userprofileid"];
                            string jobstatus = (string)rows["jobstatus"];
                            string jobdescription = (string)rows["jobpostingdescription"];
                            int biddingid = (int)rows["BiddingID"];
                            int jobid = (int)rows["jobid"];
                            int minrate = (int)(float)rows["minrate"];
                            int maxrate = (int)(float)rows["maxrate"];
                            int finalrate = (int)(float)rows["finalrate"];
                            int BidAmt = (int)rows["BidAmt"];
                            string BidStatus = (string)rows["BidStatus"];
                            jobDue = Convert.ToDateTime(rows["jobdueby"]);
                            int biddingidentry = int.Parse(biddingIDEntry.Text);

                            if (biddingidentry == biddingid)
                            {
                                await DisplayAlert("Job Info", "Job ID: " + jobid + "\nDue Date: " + jobDue + "\nBid Decision: " + jobstatus + "\nJob Description: " + jobdescription + "\nMinimum Rate: " + minrate + "\nMax Rate: " + maxrate + "\nFinal Rate: " + finalrate + "\nBid Amount: " + BidAmt + "\nBid Status: " + BidStatus + "\nBidding ID: " + biddingid, "OK");
                            }

                        }

                    }

                }
                catch (Exception e)
                {
                    await DisplayAlert("ERROR :(  ", "Something Broke! " + e, "OK");

                }
                finally
                {
                    con.Close();

                }

                //binds the data
                searchedJobsList.ItemTemplate = new DataTemplate(typeof(TextCell));
                searchedJobsList.SetValue(TextCell.TextColorProperty, Color.Black);
                searchedJobsList.SetValue(TextCell.DetailColorProperty, Color.Black);
                searchedJobsList.ItemTemplate.SetBinding(TextCell.TextProperty, "jobStatus");
                searchedJobsList.ItemTemplate.SetBinding(TextCell.DetailProperty, "jobDescription");

            };//end of search button handler


//===============================================================================================================================
//===============================================================================================================================
//============   clicked item handler     =======================================================================================
            searchedJobsList.ItemTapped += async (sender, e) =>
            {
                ListItem job = (ListItem)e.Item;
                await DisplayAlert("Job Info", "Job ID: " + job.jobID + "\nDue Date: " + job.jobDue + "\nBid Decision: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate, "OK");
                ((ListView)sender).SelectedItem = null;
            };


            //Sets the page to a stacklayout
            StackLayout stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = {
                    headerImage,
                    biddingIDLabel,
                    biddingIDEntry,
                    //searchSpacer,
                    searchButton,
                    searchSpacer1,
                    wonJobsLabel,
                    listView,
                    

                },
                //HeightRequest = 1200     //1200


            };//end of Content = new StackLayout


            ScrollView scrollView = new ScrollView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = stackLayout

            };

            //displays the stacklayout and children inside of stackLayout
            this.Content = scrollView;
            //sets the background to an image inside the JBA.Android > Resources > drawable folder
            this.BackgroundColor = Color.LightGray;


        }//end of public wonJobsPage(){}

        private Entry ToString(int jobid)
        {
            throw new NotImplementedException();
        }

        private string ToString(date date)
        {
            throw new NotImplementedException();
        }
    }
}//end of namespace JBA




