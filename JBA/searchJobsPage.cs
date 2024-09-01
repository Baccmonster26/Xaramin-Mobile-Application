using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace JBA
{
    public class searchJobsPage : ContentPage
    {
            public class ListItem{
            public string jobStatus { get; set; }
            public string jobDescript { get; set; }
            public int jobID { get; set; }
            public string jobDescription { get; set; }
            public int minRate { get; set; }
            public int maxRate { get; set; }
            public int finalRate { get; set; }

            public DateTime jobDue { get; set; }
            public int BidAmt { get; set; }
            public string BidStatus { get; set; }
            public int BidID { get; set; }

        }
        public Entry startDate { get; set; }

        public string startdate { get; set; }
        public string dateCheck { get; set; }
        public string biddingIDCheck { get; set; }
        public DateTime jobDue { get; set; }


        public searchJobsPage()
        {

        Title = "Search for Job";

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

            Label searchJobLabel = new Label
            {
                Text = "Searched Jobs",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,

            };

            Label sperate = new Label
            {
                Text = "OR",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.Black,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
            };

            Label SearchByDateRangeLabel = new Label()
            {
                Text = "Search by Date Due Date Range \n(YYYY/MM/DD): ",
                FontSize = 20,
                TextColor = Color.Black,

            };

            Label startDateLabel = new Label
            {
                Text = "Start Date:",
                FontSize = 15,
                TextColor = Color.Black,

            };

            Label endDateLabel = new Label
            {
                Text = "End Date:",
                FontSize = 15,
                TextColor = Color.Black,

            };

            DatePicker startDate = new DatePicker()
            {
                Format = "yyyy/MM/dd",
                MinimumDate = new DateTime(2020, 1, 1),
                MaximumDate = new DateTime(2025, 12, 31),

            };

            DatePicker endDate = new DatePicker()
            {
                Format = "yyyy/MM/dd",

            };

            Entry SearchByDateRangeEntry = new Entry
            {
                Placeholder = "Enter Date Range",

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
                            string dateCheck = "bad";
                            string idCheck = "bad";

                            if (startDate.Date > endDate.Date)
                            {
                                await DisplayAlert("error", "Please confirm start date is earlier then end date!", "OK");
                            }
                            else
                            {
                                dateCheck = "good";
                            }

                            if (string.IsNullOrWhiteSpace(biddingIDEntry.Text))
                            {
                                if (dateCheck == "bad")
                                {
                                    await DisplayAlert("error", "Please enter a Bidding ID!", "OK");
                                }
                            }
                            else
                            {
                                idCheck = "good";
                            }
                            MySqlConnection con;
                            con = DBUtils.CreateConnection();
                            try
                            {
                                con = DBUtils.CreateConnection();
                                con.Open();
                                searchedJob.Clear();
                                MySqlDataReader rows = DBUtils.GetAllAwardedJobs(con, "jobs");
                                while (rows.Read())
                                {
                                    if (dateCheck == "good")
                                    {
                                        string userName = (string)rows["userprofileid"];
                                        string jobstatus = (string)rows["jobstatus"];
                                        string jobdescription = (string)rows["jobpostingdescription"];
                                        int jobid = (int)rows["jobid"];
                                        int minrate = (int)(float)rows["minrate"];
                                        int maxrate = (int)(float)rows["maxrate"];
                                        int finalrate = (int)(float)rows["finalrate"];
                                        jobDue = Convert.ToDateTime(rows["jobdueby"]);
                                        if (jobDue >= startDate.Date && jobDue <= endDate.Date && jobstatus == "pending" && userName == Application.Current.Properties["LoggedIn"].ToString())
                                        {
                                            var job = new ListItem()
                                            {
                                                jobStatus = jobstatus,
                                                jobID = jobid,
                                                jobDescription = jobdescription,
                                                minRate = minrate,
                                                maxRate = maxrate,
                                                finalRate = finalrate,
                                                BidID = (int)rows["biddingid"]
                                        };
                                            searchedJob.Add(job);
                                        }
                                    }
                                    if (idCheck == "good")
                                    {
                                        string userName = (string)rows["userprofileid"];
                                        string jobstatus = (string)rows["jobstatus"];
                                        string jobdescription = (string)rows["jobpostingdescription"];
                                        int biddingid = (int)rows["biddingid"];
                                        int jobid = (int)rows["jobid"];
                                        int minrate = (int)(float)rows["minrate"];
                                        int maxrate = (int)(float)rows["maxrate"];
                                        int finalrate = (int)(float)rows["finalrate"];
                                        jobDue = Convert.ToDateTime(rows["jobdueby"]);
                                        int biddingidentry = int.Parse(biddingIDEntry.Text);

                                        if (biddingidentry == biddingid && "pending" == jobstatus)
                                        {
                                            await DisplayAlert("Job Info", "Job ID: " + jobid + "\nDue Date: " + jobDue + "\nJob Status: " + jobstatus + "\nJob Description: " + jobdescription + "\nMinimum Rate: " + minrate + "\nMax Rate: " + maxrate + "\nFinal Rate: " + finalrate + "\nBidding ID: " + biddingid, "OK");
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
                await DisplayAlert("Job Info", "Job ID: " + job.jobID + "\nDue Date: " + job.jobDue + "\nJob Status: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate + "\nBidding ID: " + job.BidID, "OK");
                ((ListView)sender).SelectedItem = null;
            };



            //Sets the page to a stacklayout
            StackLayout stackLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = {
                    headerImage,
                    SearchByDateRangeLabel,
                    startDateLabel,
                    startDate,
                    endDateLabel,
                    endDate,
                    sperate,
                    biddingIDLabel,
                    biddingIDEntry,
                    searchButton,
                    searchSpacer1,
                    searchJobLabel,
                    searchedJobsList,
  

                },
                HeightRequest = 900     //900


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
}
