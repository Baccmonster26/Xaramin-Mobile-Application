using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
namespace JBA
{
    class AwardedJobsHeaderCell : ViewCell
    {
        public AwardedJobsHeaderCell()
        {
            Label JobID_Title = new Label();
            JobID_Title.Text = "Job ID: ";
            JobID_Title.TextColor = Color.Black;
            JobID_Title.FontAttributes = FontAttributes.Bold;
            JobID_Title.FontSize = 18;
            Label JobStatus_Title = new Label();
            JobStatus_Title.Text = "Bid Status: ";
            JobStatus_Title.TextColor = Color.Black;
            JobStatus_Title.FontAttributes = FontAttributes.Bold;
            JobStatus_Title.FontSize = 18;
            Label BidAmt_Title = new Label();
            BidAmt_Title.Text = "Bid Amount: ";
            BidAmt_Title.TextColor = Color.Black;
            BidAmt_Title.FontAttributes = FontAttributes.Bold;
            BidAmt_Title.FontSize = 18;
            Label JobID = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center
            };
            JobID.SetBinding(Label.TextProperty, "ID");
            Label JobStatus = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            JobStatus.SetBinding(Label.TextProperty, "BidStatus");
            Label BidAmt = new Label
            {
                FontSize = 18,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
            };
            BidAmt.SetBinding(Label.TextProperty, "BidAmt");
            StackLayout viewLayout1 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { JobID_Title, JobID },
            };
            StackLayout viewLayout2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { JobStatus_Title, JobStatus },
            };
            StackLayout viewLayout3 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                BackgroundColor = Color.White,
                Children = { BidAmt_Title, BidAmt }
            };
            StackLayout viewLayout4 = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                BackgroundColor = Color.White,
                Children = { viewLayout1, viewLayout2, viewLayout3 }
            };
            View = viewLayout4;
        }
    }
}
