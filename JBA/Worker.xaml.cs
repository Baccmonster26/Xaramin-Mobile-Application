using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JBA
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Worker : ContentPage
    {
        public Worker()
        {
            InitializeComponent();
            InitializeComponent();
            ToolbarItems.Clear();
            ToolbarItem Logout = new ToolbarItem
            {
                Text = "Logout",
                Order = ToolbarItemOrder.Secondary,
            };
            Logout.Clicked += OnLogoutClicked;
            this.ToolbarItems.Add(Logout);
            Title = "Welcome Back!";

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


            BoxView headSpacer = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 30,

            };

            Button wonJobsButton = new Button
            {
                Text = "Won Jobs",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(108, 25, 111, 25),
                BorderWidth = 2,
                BorderColor = Color.Black,
                BackgroundColor = Color.FromRgb(164, 165, 166),
                CornerRadius = 5,

            };

            Button SearchedJobsButton = new Button
            {
                Text = "Search Jobs",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(108, 25, 111, 25),
                BorderWidth = 2,
                BorderColor = Color.Black,
                BackgroundColor = Color.FromRgb(164, 165, 166),
                CornerRadius = 5,

            };

            BoxView buttonSpacer = new BoxView
            {
                Opacity = 0,
                WidthRequest = 5,
                HeightRequest = 20,

            };

            Button pendingJobButton = new Button
            {
                Text = "Pending Jobs",
                FontSize = 20,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,    //HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(90, 25, 90, 25),
                BorderWidth = 2,
                BorderColor = Color.Black,
                BackgroundColor = Color.FromRgb(164, 165, 166),
                CornerRadius = 5,

            };

            //=================================================================================================================
            //=================================================================================================================
            //=================================================================================================================
            //=========    handler for when the button is clicked     =========================================================
            pendingJobButton.Clicked += async (sender, args) => {
                await Navigation.PushAsync(new pendingJobPage());

            };

            //handler for when the button is clicked
            wonJobsButton.Clicked += async (sender, args) => {
                await Navigation.PushAsync(new wonJobsPage());

            };

            SearchedJobsButton.Clicked += async (sender, args) => {
                await Navigation.PushAsync(new searchJobsPage());

            };

            //=================================================================================================================
            //=================================================================================================================
            //=================================================================================================================
            //=========     layout of the page and view and calls them to be displayed.      ================================== 
            StackLayout stackLayout = new StackLayout
            {
                Children = {

                    headerImage,
                    headSpacer,
                    wonJobsButton,
                    buttonSpacer,
                    pendingJobButton,
                    buttonSpacer,
                    SearchedJobsButton
                }
            };


            //code to show the views and layouts to the user. 
            this.Content = stackLayout;
            this.BackgroundColor = Color.LightGray;

        }
        async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}