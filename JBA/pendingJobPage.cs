using Java.Sql;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;



namespace JBA
{
    class pendingJobPage : ContentPage
    {
        public MySqlConnection con { get; private set; }

        public class ListItem
        {
            public string jobStatus { get; set; }
            public string jobDescript { get; set; }
            public int jobID { get; set; }
            public string jobDescription { get; set; }
            public int minRate { get; set; }
            public int maxRate { get; set; }
            public int finalRate { get; set; }

            public DateTime jobDue { get; set; }
            public int jobGroupID { get; set; }
            public int biddingID { get; set; }
            public string userProfileID { get; set; }
            public DateTime dateFinished { get; set; }
            public DateTime dateStarted { get; set; }

            public int BidAmt { get; set; }
            public string BidStatus { get; set; }


        }
        public Entry startDate { get; set; }

        public string startdate { get; set; }
        public string dateCheck { get; set; }
        public string biddingIDCheck { get; set; }
        public DateTime jobDue { get; set; }

        public pendingJobPage()
        {
            Title = "Pending Bids";

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=========     Labels, buttons, entries     ======================================================================
            Image headerImage = new Image
            {
                Source = "greenhouse_update.jpg",
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = 100,

            };

            BoxView pendingSpacer = new BoxView {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 5,
            };

            BoxView rejectedSpacer = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 5,
            };

            Label pendingBidsLabel = new Label {
                Text = "Pending Bids",
                FontSize = 25,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,
            };

            Label rejectedBidsLabel = new Label {
                Text = "Rejected Bids",
                FontSize = 25,
                FontAttributes = FontAttributes.Bold,
                TextDecorations = TextDecorations.Underline,


            };

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=========     Gets list for undecided (running) jobs    =========================================================

            var runningList = new ListView();
            var runningJobs = new ObservableCollection<ListItem> { };
            runningList.ItemsSource = runningJobs;
            runningList.RowHeight = 60;
            runningList.SeparatorColor = Color.Black;
            runningList.SetValue(TextCell.TextColorProperty, Color.Black);
            runningList.SetValue(TextCell.DetailColorProperty, Color.Black);

            MySqlConnection con;
            con = DBUtils.CreateConnection();

            try
            {

                con.Open();
                MySqlDataReader rows = DBUtils.GetPendingBids(con, "bidders");
                DateTime jobdue;
                string userName = Application.Current.Properties["LoggedIn"].ToString();
                while (rows.Read())
                {
                    int jobid = (int)rows["jobid"];
                    int jobgroupid = (int)rows["jobgroupid"];
                    int biddingid = (int)rows["biddingid"];
                    string userprofileid = (string)rows["userprofileid"];
                    string jobstatus = (string)rows["jobstatus"];
                    string jodescription = (string)rows["jobpostingdescription"];
                    int minrate = (int)(float)rows["minrate"];
                    int maxrate = (int)(float)rows["maxrate"];
                    jobdue = Convert.ToDateTime(rows["jobdueby"]);
                    int finalrate = (int)(float)rows["finalrate"];
                    if (userName == (string)rows["userprofileid"] && "pending" == (string)rows["jobstatus"])
                    {
                        var pendingJob = new ListItem()
                        {
                            jobID = jobid,
                            jobStatus = jobstatus,
                            jobDescription = jodescription,
                            minRate = minrate,
                            maxRate = maxrate,
                            finalRate = finalrate,
                            jobDue = jobdue,
                            jobGroupID = jobgroupid,
                            biddingID = biddingid,
                            userProfileID = userprofileid,
                            BidAmt = (int)rows["BidAmt"],
                            BidStatus = (string)rows["BidStatus"]


                        };
                        runningJobs.Add(pendingJob);
                    }
                }
            }
            catch (Exception e)
            {
                DisplayAlert("ERROR0 :(  ", "Something Broke! " + e, "OK");
            }
            finally
            {
                con.Close();
                
            };

            //binds the data
            runningList.ItemTemplate = new DataTemplate(typeof(TextCell));
            runningList.SetValue(TextCell.TextColorProperty, Color.Black);
            runningList.SetValue(TextCell.DetailColorProperty, Color.Black);
            runningList.ItemTemplate.SetBinding(TextCell.TextProperty, "jobStatus");
            runningList.ItemTemplate.SetBinding(TextCell.DetailProperty, "jobDescription");

            var rejectedList = new ListView();
            var rejectedJobs = new ObservableCollection<ListItem> { };
            rejectedList.ItemsSource = rejectedJobs;
            rejectedList.RowHeight = 60;
            rejectedList.SeparatorColor = Color.Black;
            rejectedList.SetValue(TextCell.TextColorProperty, Color.Black);
            rejectedList.SetValue(TextCell.DetailColorProperty, Color.Black);

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=========     running list clicked handler    ===================================================================

            runningList.ItemTapped += async (sender, e) =>
            {
                ListItem job = (ListItem)e.Item;
                ((ListView)sender).SelectedItem = null;

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=========     handler to edit status of job     =================================================================

                bool change = await DisplayAlert("Job Info", "Job ID:  " + job.jobID + "\njob status: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate + "\nBid Amount: " + job.BidAmt + "\nBid Status: " + job.BidStatus, "Accept/Reject", "Close");

                if (change == true) {
                    string action = await DisplayActionSheet("Change Decision?", "Cancel", null, "Accept", "Reject");
                   con = DBUtils.CreateConnection();
                    if (action == "Accept")
                    {
                        try
                        {
                            con.Open();
                            MySqlDataReader rows = DBUtils.insertAcceptedJob(con, job.biddingID);
                        }
                        catch (Exception z)
                        {
                            await DisplayAlert("ERROR0 :(  ", "Something Broke! " + z, "OK");
                        }
                        finally
                        {
                            con.Close();

                        };
                        Navigation.InsertPageBefore(new pendingJobPage(), this);
                        await Navigation.PopAsync();
                    }

                    else
                    {
                        try
                        {
                            con.Open();

                            MySqlDataReader rows = DBUtils.insertRejectedJob(con, job.biddingID);
                            DateTime jobdue;
                            string userName = Application.Current.Properties["LoggedIn"].ToString();
                            while (rows.Read())
                            {
                                int jobid = (int)rows["jobid"];
                                jobdue = Convert.ToDateTime(rows["jobdueby"]);
                                string jobstatus = (string)rows["jobstatus"];
                                string jodescription = (string)rows["jobpostingdescription"];
                                int minrate = (int)(float)rows["minrate"];
                                int maxrate = (int)(float)rows["maxrate"];
                                int finalrate = (int)(float)rows["finalrate"];
                                if (userName == (string)rows["userprofileid"] && "rejected" == (string)rows["jobstatus"])
                                {
                                    var rejectedJob = new ListItem()
                                    {

                                        jobID = jobid,
                                        jobStatus = jobstatus,
                                        jobDescription = jodescription,
                                        minRate = minrate,
                                        maxRate = maxrate,
                                        finalRate = finalrate,
                                        jobDue = jobdue,
                                        biddingID = (int)rows["BiddingID"],
                                        BidAmt = (int)rows["BidAmt"],
                                        BidStatus = (string)rows["BidStatus"]
                                    };
                                    rejectedJobs.Add(rejectedJob);
                                }
                            }
                        }

                        catch (Exception z)
                        {
                            await DisplayAlert("ERROR0 :(  ", "Something Broke! " + z, "OK");
                        }
                        finally
                        {
                            con.Close();

                        };
                        Navigation.InsertPageBefore(new pendingJobPage(), this);
                        await Navigation.PopAsync();
                    }
            }

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=======     Extra feature if have time    =======================================================================

                    //await DisplayAlert("Message", "Job ID:  " + job.jobID + "\njob status: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate, "OK");
                    /*               bool edit = await DisplayAlert("Question?", "edit? ", "true", "false");
                                   //await DisplayAlert("yup", "edit is: " + edit, "ok" );

                                   if(edit == true) {
                                       string minrateanswer = await DisplayPromptAsync("Editing", "Minimum Rate?");
                                       int minRateAnswer = int.Parse(minrateanswer);

                                       string maxrateanswer = await DisplayPromptAsync("Editing", "Maximum Rate?");
                                       int maxRateAnswer = int.Parse(maxrateanswer);

                                       string finalrateanswer = await DisplayPromptAsync("Editing", "Final Rate?");
                                       int finalRateAnswer = int.Parse(finalrateanswer);

                                       //await DisplayAlert("yup", "min: " + minRateAnswer ,"ok"  );
                                       con.Open();
                                       MySqlDataReader row = DBUtils.editRow(con, "jobs", job.jobID, minRateAnswer, maxRateAnswer, finalRateAnswer);

                                       try
                                       {                
                                           while (row.Read())
                                           {

                                               int jobid = (int)row["jobid"];
                                               string jobstatus = (string)row["jobstatus"];
                                               string jodescription = (string)row["jobpostingdescription"];
                                               string userName = "user";

                                               if (userName == (string)row["userprofileid"] && "running" == (string)row["jobstatus"])
                                               {
                                                   var running = new ListItem()
                                                   {
                                                       jobID = (int)row["jobid"],
                                                       jobStatus = (string)row["jobstatus"],
                                                       jobDescription = (string)row["jobpostingdescription"],
                                                   };
                                                   runningJobs.Add(running);
                                               }


                                               if (userName == (string)row["userprofileid"] && "rejected" == (string)row["jobstatus"])
                                               {

                                                   var rejected = new ListItem()
                                                   {
                                                       jobID = (int)row["jobid"],
                                                       jobStatus = (string)row["jobstatus"],
                                                       jobDescription = (string)row["jobpostingdescription"],
                                                   };
                                                   rejectedJobs.Add(rejected);

                                               }




                                           }

                                       }
                                       catch (Exception f)
                                       {
                                           await DisplayAlert("ERROR2 :(  ", "Something Broke! " + f, "OK");
                                       }
                                       finally
                                       {

                                           con.Close();
                                       }
                                       Navigation.InsertPageBefore(new pendingJobPage(), this);
                                       await Navigation.PopAsync();
                                       //binds the data
                                       runningList.ItemTemplate = new DataTemplate(typeof(TextCell));
                                       runningList.SetValue(TextCell.TextColorProperty, Color.Black);
                                       runningList.SetValue(TextCell.DetailColorProperty, Color.Black);
                                       runningList.ItemTemplate.SetBinding(TextCell.TextProperty, "jobStatus");
                                       runningList.ItemTemplate.SetBinding(TextCell.DetailProperty, "jobDescription");

                                   }

                   */




                };



//=================================================================================================================
//=================================================================================================================
//=================================================================================================================

/*
            var rejectedList = new ListView();
            var rejectedJobs = new ObservableCollection<ListItem> { };
            rejectedList.ItemsSource = rejectedJobs;
            rejectedList.RowHeight = 60;
            rejectedList.SeparatorColor = Color.Black;
            rejectedList.SetValue(TextCell.TextColorProperty, Color.Black);
            rejectedList.SetValue(TextCell.DetailColorProperty, Color.Black);
  */  

            try
            {

                con.Open();
                MySqlDataReader rows = DBUtils.GetRejectedBids(con, "bidders");
                string userName = Application.Current.Properties["LoggedIn"].ToString();
                DateTime jobdue;
                //string status = "pending";
                while (rows.Read())
                {
                    int jobid = (int)rows["jobid"];
                    int minrate = (int)(float)rows["minrate"];
                    int maxrate = (int)(float)rows["maxrate"];
                    int finalrate = (int)(float)rows["finalrate"];
                    string jobstatus = (string)rows["jobstatus"];
                    string jodescription = (string)rows["jobpostingdescription"];
                    jobdue = Convert.ToDateTime(rows["jobdueby"]);
                    if (userName == (string)rows["userprofileid"] && "pending" == (string)rows["jobstatus"])
                    {
                        var rejectedJob = new ListItem()
                        {
                            jobStatus = jobstatus,
                            jobDescription = jodescription,
                            jobID = jobid,
                            minRate = minrate,
                            maxRate = maxrate,
                            finalRate = finalrate,
                            jobDue = jobdue,
                            biddingID = (int)rows["BiddingID"],
                            BidAmt = (int)rows["BidAmt"],
                            BidStatus = (string)rows["BidStatus"]


                        };
                        rejectedJobs.Add(rejectedJob);
                    }
                }
            }
            catch (Exception e)
            {
                DisplayAlert("ERROR1 :(  ", "Something Broke! " + e, "OK");
            }
            finally
            {
                con.Close();
                
            };

            //binds the data
            rejectedList.ItemTemplate = new DataTemplate(typeof(TextCell));
            rejectedList.SetValue(TextCell.TextColorProperty, Color.Black);
            rejectedList.SetValue(TextCell.DetailColorProperty, Color.Black);
            rejectedList.ItemTemplate.SetBinding(TextCell.TextProperty, "BidStatus");
            rejectedList.ItemTemplate.SetBinding(TextCell.DetailProperty, "jobDescription");

//=================================================================================================================
//=================================================================================================================
//=================================================================================================================
//=========     rejected list handler if clicked     ==============================================================

            rejectedList.ItemTapped += async (sender, e) =>
            {
                ListItem job = (ListItem)e.Item;
                bool delete = await DisplayAlert("Job Info", "Job ID:  " + job.jobID + "\njob status: " + job.jobStatus + "\njob Description: " + job.jobDescription + "\nMinimum Rate: " + job.minRate + "\nMaximum Rate: " + job.maxRate + "\nFinal Rate: " + job.finalRate + "\nBid Amount: " + job.BidAmt + "\nBid Status: " + job.BidStatus, "Delete", "Close");
                if (delete == true)
                {
                    bool confirm = await DisplayAlert("Confirm", "Are you sure you would like to delete this bid?", "yes", "no");
                    if (confirm == true)
                    {
                       try
                        {
                            con.Open();
                            MySqlDataReader row = DBUtils.deleteRow(con, "bidders", job.biddingID);                  
                        }
                        catch (Exception f)
                        {
                            await DisplayAlert("ERROR2 :(  ", "Something Broke! " + f, "OK");
                        }
                        finally
                        {
                            
                            con.Close();
                        }
                        Navigation.InsertPageBefore(new pendingJobPage(), this);
                        await Navigation.PopAsync();

                    }

                }

                ((ListView)sender).SelectedItem = null;
            };


//=================================================================================================================
//=================================================================================================================
//=================================================================================================================


            StackLayout stackLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Orientation = StackOrientation.Vertical,
                    Children =
                {
                    headerImage,
                    pendingSpacer,
                    pendingBidsLabel,
                    runningList,
                    rejectedSpacer,
                    rejectedBidsLabel,
                    rejectedList

                },

                };

                ScrollView scrollView = new ScrollView
                {
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Content = stackLayout

                };

                this.Content = scrollView;
                this.BackgroundColor = Color.LightGray;

            }



            }//end of public class pendingJobPage : ContentPage

    } 

 
