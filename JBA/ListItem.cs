using System;
using System.Collections.Generic;
using System.Text;

namespace JBA
{
    class ListItem
    {
        public int ID { get; set; }
        public int RobotID { get; set; }
        public string Serialnumber { get; set; }
        public int Organizationid { get; set; }
        public int Greenhouseid { get; set; }
        public string Robotname { get; set; }
        public string Status { get; set; }
        public string Deploystatus { get; set; }
        public int jobgroupid { get; set; }
        public int jobid { get; set; }
        public string jobpostingdescription { get; set; }
        public string jobstatus { get; set; }
        public int biddingid { get; set; }
        public string userprofileid { get; set; }
        public string jobdueby { get; set; }
        public int BidAmt { get; set; }
        public string BidStatus { get; set; }
        public int userid { get; set; }
        public string user_id { get; set; }
        public string planttype { get; set; }
        public int plantsperpod { get; set; }
        public string BidStatus2 { get; set; }
        public string Availability { get; set; }
        public int inventoryID { get; set; }
        public string location { get; set; }
        public DateTime harvestDate { get; set; }
        public string harvestDetail { get; set; }
        public int itemsStored { get; set; }
        public string onOrder { get; set; }
        public int deleted { get; set; }
        public int harvestID { get; set; }
        public string enteredBy { get; set; }

    }
}
