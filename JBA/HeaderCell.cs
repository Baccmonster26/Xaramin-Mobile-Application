using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MySqlConnector;
namespace JBA
{
    class HeaderCell : ViewCell
    {
        public HeaderCell()
        {
            this.Height = 30;
            Label RobotID = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            RobotID.SetBinding(Label.TextProperty, "ID");
            Label Serialnumber = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            Serialnumber.SetBinding(Label.TextProperty, "Serialnumber");
            Label Organizationid = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            Organizationid.SetBinding(Label.TextProperty, "Organizationid");
            Label Greenhouseid = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            Greenhouseid.SetBinding(Label.TextProperty, "Greenhouseid");

            Label Robotname = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            Robotname.SetBinding(Label.TextProperty, "Robotname");
            Label Description = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            Description.SetBinding(Label.TextProperty, "Description");
            Label Status = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            Status.SetBinding(Label.TextProperty, "Status");
            Label Deploystatus = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            Deploystatus.SetBinding(Label.TextProperty, "Deploystatus");
            Label jobgroupid = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            jobgroupid.SetBinding(Label.TextProperty, "jobgroupid");
            Label jobpostingdescription = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            jobpostingdescription.SetBinding(Label.TextProperty, "jobpostingdescription");
            Label jobstatus = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            jobstatus.SetBinding(Label.TextProperty, "jobstatus");
            Label biddingid = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            biddingid.SetBinding(Label.TextProperty, "biddingid");
            Label userprofileid = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            userprofileid.SetBinding(Label.TextProperty, "userprofileid");
            Label BidStatus = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            BidStatus.SetBinding(Label.TextProperty, "BidStatus");
            Label planttype = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            planttype.SetBinding(Label.TextProperty, "planttype");
            StackLayout viewLayoutItem = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = 15,
                Spacing = 15,
                Children = { RobotID, Serialnumber, jobstatus, BidStatus, planttype }
            };

            StackLayout viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { viewLayoutItem },
            };

            View = viewLayout;
        }
    }
}
