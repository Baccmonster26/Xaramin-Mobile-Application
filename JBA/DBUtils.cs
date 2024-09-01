using System;
using MySqlConnector;

namespace JBA
{
    public static class DBUtils
    {
        public static MySqlConnection CreateConnection()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "",
                Database = "",
                UserID = "",
                Password = "",
            };
            MySqlConnection con = new MySqlConnection(builder.ConnectionString);
            return con;
        }

        public static MySqlDataReader GetAllProducts(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table;
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetAllPendingProducts(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN bidders ON" +
                " jobs.biddingid = bidders.BiddingID " +
                "WHERE jobstatus = 'pending'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetAllRobotStatus(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN robots ON" +
                " jobgroups.robotid = robots.robotid " +
                "WHERE jobgroups.deleted = 0 AND jobgroups.jobgroupstatus != 'Not Started'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetAllPendingOrders(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " WHERE BidStatus = 'running'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetSelectedPlant(MySqlConnection con, string table, string plant)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " WHERE harvestDetail = '" +
                plant + "'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetAllRunningProducts(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " WHERE jobstatus = 'running'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetAllAwardedProducts(MySqlConnection con, string table)
        {
            DateTime CurrentDate = DateTime.Now;
            DateTime TwoWeeksBefore = CurrentDate.AddDays(-14);
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'completed' AND jobs.jobdueby BETWEEN '" + TwoWeeksBefore.ToString("yyyy-MM-dd") + 
                "' AND '" + CurrentDate.ToString("yyyy-MM-dd") + "'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }

        public static MySqlDataReader GetSelectedAwardedProducts(MySqlConnection con, string table, string date)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'completed' AND jobs.jobdueby = '" + date + "'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static MySqlDataReader GetSelectedRangedAwardedProducts(MySqlConnection con, string table, string date, string date2)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'completed' AND jobs.jobdueby BETWEEN '" + date + "' " +
                "AND '" + date2 + "'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static MySqlDataReader deleteRow(MySqlConnection con, string table, int jobID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE `bidders` SET Deleted = '1' WHERE '" + jobID + "' = BiddingID";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static int EditPassword(MySqlConnection conn, string table, string input, string user, string oldpass)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE " + table +
                " SET password = '" + input + "' WHERE username = " + "'" + user + "'" +
                " AND password = " + "'" + oldpass + "'";
            cmd.Connection = conn;
            int rowCnt = cmd.ExecuteNonQuery();
            if (rowCnt > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        /*
        public static MySqlDataReader editRow(MySqlConnection con, string table, int jobID, int minRate, int maxRate, int finalRate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE job SET minrate = '" + minRate + "' , maxrate = '" + maxRate + "', finarate = '" + finalRate +"' WHERE jobid = '" + jobID + "'";
            cmd.Connection = con;
            MySqlDataReader row = cmd.ExecuteReader();
            return row;
        }
        */
        public static MySqlDataReader searchByDueDate(MySqlConnection con, date startDate, date endDate)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM job WHERE jobdueby <= " + endDate + " && jobdueby >= " + startDate + "'";
            cmd.Connection = con;
            MySqlDataReader row = cmd.ExecuteReader();
            return row;
        }

        public static MySqlDataReader insertAcceptedJob(MySqlConnection con, int biddingID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE `bidders` SET BidStatus = 'completed' WHERE '" + biddingID + "' = BiddingID";
            cmd.Connection = con;
            MySqlDataReader row = cmd.ExecuteReader();
            return row;
        }


        public static MySqlDataReader insertRejectedJob(MySqlConnection con, int biddingID)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "UPDATE `bidders` SET BidStatus = 'reject' WHERE '" + biddingID + "' = BiddingID";
            cmd.Connection = con;
            MySqlDataReader row = cmd.ExecuteReader();
            return row;
        }
        public static MySqlDataReader GetAllAwardedJobs(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " ORDER BY jobdueby DESC";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static MySqlDataReader GetPendingBids(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'running'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static MySqlDataReader GetWonBids(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'completed'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
        public static MySqlDataReader GetRejectedBids(MySqlConnection con, string table)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + table + " INNER JOIN jobs " +
                "ON bidders.BiddingID = jobs.biddingid " +
                "WHERE BidStatus = 'reject' AND bidders.Deleted = '0'";
            cmd.Connection = con;
            MySqlDataReader rows = cmd.ExecuteReader();
            return rows;
        }
    }
}
