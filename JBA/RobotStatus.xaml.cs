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
    public partial class RobotStatus : ContentPage
    {
        MySqlConnection conn;
        public RobotStatus()
        {
            InitializeComponent();
            this.Title = "Robot Availability";
            var listView = new ListView();
            string ITable = "jobgroups";
            TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
            string RobotStatus = "";
            var items = new ObservableCollection<ListItem>{};
            listView.ItemsSource = items;
            conn = DBUtils.CreateConnection();
            try
            {
                conn.Open();
                MySqlDataReader rows = DBUtils.GetAllRobotStatus(conn, ITable);
                while (rows.Read())
                {
                    if(CurrentTime >= (TimeSpan)rows["starttime"] && CurrentTime <= (TimeSpan)rows["endtime"] && (string)rows["jobgroupstatus"] == "pending")
                    {
                        RobotStatus = "In Use";
                    }
                    else
                    {
                        RobotStatus = "Available";
                    }
                    string serialnumber = (string)rows["serialnumber"];
                    string robotname = (string)rows["robotname"];
                    string status = RobotStatus;
                    string deploy = (string)rows["deploystatus"];
                    var thing = new ListItem()
                    {
                        RobotID = (int)rows["robotid"],
                        Serialnumber = serialnumber,
                        Organizationid = (int)rows["organizationid"],
                        Greenhouseid = (int)rows["greenhouseid"],
                        Robotname = robotname,
                        Status = status,
                        Deploystatus = deploy
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
            listView.ItemTemplate = new DataTemplate(typeof(RobotStatusHeaderCell));
            listView.SeparatorColor = Color.Black;

            listView.ItemTapped += async (sender, e) =>
            {
                ListItem item = (ListItem)e.Item;
                await DisplayAlert("Tapped", "Robot " + item.RobotID + " was selected." + "\n" + 
                    "Robot Status: " + item.Status, "OK");
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
                    MySqlDataReader rows = DBUtils.GetAllRobotStatus(conn, ITable);
                    while (rows.Read())
                    {
                        if (CurrentTime >= (TimeSpan)rows["starttime"] && CurrentTime <= (TimeSpan)rows["endtime"])
                        {
                            RobotStatus = "In Use";
                        }
                        else
                        {
                            RobotStatus = "Available";
                        }
                        string serialnumber = (string)rows["serialnumber"];
                        string robotname = (string)rows["robotname"];
                        string status = RobotStatus;
                        string deploy = (string)rows["deploystatus"];
                        var thing = new ListItem()
                        {
                            RobotID = (int)rows["robotid"],
                            Serialnumber = serialnumber,
                            Organizationid = (int)rows["organizationid"],
                            Greenhouseid = (int)rows["greenhouseid"],
                            Robotname = robotname,
                            Status = status,
                            Deploystatus = deploy
                        };
                        items.Add(thing);
                    }
                }
                catch (Exception e)
                {
                    await DisplayAlert("Error", "ERROR: Did not query AWS DB sucessfulyy!" + "\n" + e, "OK");
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