using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MySqlConnector;
namespace JBA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Organization : TabbedPage
    {
        public Organization()
        {
            InitializeComponent();
            ToolbarItems.Clear();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Robot Availability",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new RobotStatus()))
            });
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Inventory of Items",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(() => Navigation.PushAsync(new Inventory()))
            });
            ToolbarItem Logout = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Secondary,
            };
            Logout.Clicked += OnLogoutClicked;
            this.ToolbarItems.Add(Logout);
            this.Title = "Organization Section";

            this.Children.RemoveAt(0);
            this.Children.RemoveAt(0);
            this.Children.RemoveAt(0);
            this.Children.Add(new PendingJobs() { Title = "Pending Jobs" });
            this.Children.Add(new AwardedJobs() { Title = "Awarded Jobs" });
            this.Children.Add(new JobsInProgress() { Title = "Jobs in Progress" });
            this.CurrentPage = this.Children[0];
        }

        async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}