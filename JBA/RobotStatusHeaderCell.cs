using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace JBA
{
    class RobotStatusHeaderCell : ViewCell
    {
        public RobotStatusHeaderCell()
        {
            Label RobotID_Title = new Label();
            RobotID_Title.Text = "Robot ID: ";
            RobotID_Title.TextColor = Color.Black;
            RobotID_Title.FontAttributes = FontAttributes.Bold;
            RobotID_Title.FontSize = 18;
            Label serialnumber_Title = new Label();
            serialnumber_Title.Text = "Serial Number: ";
            serialnumber_Title.TextColor = Color.Black;
            serialnumber_Title.FontAttributes = FontAttributes.Bold;
            serialnumber_Title.FontSize = 18;
            Label Status_Title = new Label();
            Status_Title.Text = "Robot Availability: ";
            Status_Title.TextColor = Color.Black;
            Status_Title.FontAttributes = FontAttributes.Bold;
            Status_Title.FontSize = 18;
            Label RobotID = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            RobotID.SetBinding(Label.TextProperty, "RobotID");
            Label serialnumber = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            serialnumber.SetBinding(Label.TextProperty, "Serialnumber");
            Label Status = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            Status.SetBinding(Label.TextProperty, "Status");
            StackLayout viewLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { RobotID_Title, RobotID }
            };
            StackLayout viewLayout2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { serialnumber_Title, serialnumber },
            };
            StackLayout viewLayout3 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { Status_Title, Status }
            };
            StackLayout viewLayout4 = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children = { viewLayout, viewLayout2, viewLayout3 }
            };
            View = viewLayout4;
        }
    }
}
