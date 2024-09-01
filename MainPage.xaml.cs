using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySqlConnector;
using System.Security.Cryptography;
namespace JBA
{

    public partial class MainPage : ContentPage
    {
        MySqlConnection con;

        static byte[] hmacSHA256(String data, String key)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        public static string CreateMD5(byte[] input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(input);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            con = DBUtils.CreateConnection();
            NavigationPage.SetHasNavigationBar(this, false);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                DisplayAlert("ALERT", "Unable to connect to database!" + "\n" + e, "OK");
            }
            finally
            {
                con.Close();
            }
        }
        async void OnLoginClicked(object sender, EventArgs args)
        {
            if (Username.Text == null)
            {
                await DisplayAlert("ALERT", "Please enter in a username!", "OK");
            }
            else
            {
                if (Password.Text == null)
                {
                    await DisplayAlert("ALERT", "Please enter in a password!", "OK");
                }
                else
                {
                    string encoded = CreateMD5(hmacSHA256(Password.Text, "crtedg#$%")).Replace("-", "").ToLower();
                    int i = 0;
                    int j = 0;
                    con = DBUtils.CreateConnection();
                    try
                    {
                        con.Open();
                        MySqlDataReader rows = DBUtils.GetAllProducts(con, "login");
                        while (rows.Read())
                        {
                            if (Username.Text == (string)rows["username"] && encoded == (string)rows["password"])
                            {
                                j++;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        await DisplayAlert("ALERT", "Unable to connect to database!" + "\n" + e, "OK");
                    }
                    finally
                    {
                        con.Close();
                    }
                    if (j == 1)
                    {
                        string action = await DisplayActionSheet("Do you want to change your password?", "Cancel", null, "Yes", "No");
                        if (action == "Yes")
                        {
                            string action2 = await DisplayActionSheet("Are you sure you want to change your password?", "Cancel", null, "Yes", "No");
                            if (action2 == "Yes")
                            {
                                string answer2 = await App.Current.MainPage.DisplayPromptAsync("Password Change", "What do you want your new password to be?", "Ok");
                                string newpass = CreateMD5(hmacSHA256(answer2, "crtedg#$%")).Replace("-", "").ToLower();
                                try
                                {
                                    con.Open();
                                    DBUtils.EditPassword(con, "login", newpass, Username.Text, encoded);
                                }
                                catch (Exception e)
                                {
                                    await DisplayAlert("ALERT", "Unable to connect to database!" + "\n" + e, "OK");
                                }
                                finally
                                {
                                    con.Close();
                                }
                                await DisplayAlert("ALERT", "Sucessfully changed password!", "OK");
                                try
                                {
                                    con.Open();
                                    MySqlDataReader rows = DBUtils.GetAllProducts(con, "login");
                                    while (rows.Read())
                                    {
                                        if (Username.Text == (string)rows["username"] && newpass == (string)rows["password"])
                                        {
                                            string designation = (string)rows["usertype"];
                                            if (designation == "picker")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                Application.Current.MainPage = new NavigationPage(new Worker());
                                            }
                                            else if (designation == "orguser")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                Application.Current.MainPage = new NavigationPage(new Organization());
                                            }
                                            else if (designation == "admin")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");
                                                string answer = await DisplayActionSheet("Send to?", "Cancel", null, "Worker", "Organization");
                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                if (answer == "Worker")
                                                {
                                                    Application.Current.MainPage = new NavigationPage(new Worker());
                                                }
                                                if (answer == "Organization")
                                                {
                                                    Application.Current.MainPage = new NavigationPage(new Organization());
                                                }
                                            }
                                            else
                                            {
                                                await DisplayAlert("ALERT", "Something went wrong!", "OK");
                                            }
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    await DisplayAlert("ALERT", "Unable to query account database!" + "\n" + e, "OK");
                                }
                                finally
                                {
                                    con.Close();
                                }
                            }
                            else if (action2 == "No")
                            {
                                try
                                {
                                    con.Open();
                                    MySqlDataReader rows = DBUtils.GetAllProducts(con, "login");
                                    while (rows.Read())
                                    {
                                        if (Username.Text == (string)rows["username"] && encoded == (string)rows["password"])
                                        {
                                            string designation = (string)rows["usertype"];
                                            if (designation == "picker")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                Application.Current.MainPage = new NavigationPage(new Worker());
                                            }
                                            else if (designation == "orguser")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                Application.Current.MainPage = new NavigationPage(new Organization());
                                            }
                                            else if (designation == "admin")
                                            {
                                                Application.Current.Properties["designation"] = designation;
                                                await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");
                                                string answer = await DisplayActionSheet("Send to?", "Cancel", null, "Worker", "Organization");
                                                Application.Current.Properties["LoggedIn"] = Username.Text;
                                                var loginName = Application.Current.Properties["LoggedIn"] as string;
                                                i++;
                                                if (answer == "Worker")
                                                {
                                                    Application.Current.MainPage = new NavigationPage(new Worker());
                                                }
                                                if (answer == "Organization")
                                                {
                                                    Application.Current.MainPage = new NavigationPage(new Organization());
                                                }
                                            }
                                            else
                                            {
                                                await DisplayAlert("ALERT", "Something went wrong!", "OK");
                                            }
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    await DisplayAlert("ALERT", "Unable to query account database!" + "\n" + e, "OK");
                                }
                                finally
                                {
                                    con.Close();
                                }
                            }
                        }
                        else if (action == "No")
                        {
                            try
                            {
                                con.Open();
                                MySqlDataReader rows = DBUtils.GetAllProducts(con, "login");
                                while (rows.Read())
                                {
                                    if (Username.Text == (string)rows["username"] && encoded == (string)rows["password"])
                                    {
                                        string designation = (string)rows["usertype"];
                                        if (designation == "picker")
                                        {
                                            Application.Current.Properties["designation"] = designation;
                                            await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                            Application.Current.Properties["LoggedIn"] = Username.Text;
                                            var loginName = Application.Current.Properties["LoggedIn"] as string;
                                            i++;
                                            Application.Current.MainPage = new NavigationPage(new Worker());
                                        }
                                        else if (designation == "orguser")
                                        {
                                            Application.Current.Properties["designation"] = designation;
                                            await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");

                                            Application.Current.Properties["LoggedIn"] = Username.Text;
                                            var loginName = Application.Current.Properties["LoggedIn"] as string;
                                            i++;
                                            Application.Current.MainPage = new NavigationPage(new Organization());
                                        }
                                        else if (designation == "admin")
                                        {
                                            Application.Current.Properties["designation"] = designation;
                                            await DisplayAlert("ALERT", "Sucessfully logged in as " + designation, "OK");
                                            string answer = await DisplayActionSheet("Send to?", "Cancel", null, "Worker", "Organization");
                                            Application.Current.Properties["LoggedIn"] = Username.Text;
                                            var loginName = Application.Current.Properties["LoggedIn"] as string;
                                            i++;
                                            if (answer == "Worker")
                                            {
                                                Application.Current.MainPage = new NavigationPage(new Worker());
                                            }
                                            if (answer == "Organization")
                                            {
                                                Application.Current.MainPage = new NavigationPage(new Organization());
                                            }
                                        }
                                        else
                                        {
                                            await DisplayAlert("ALERT", "Something went wrong!", "OK");
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                await DisplayAlert("ALERT", "Unable to query account database!" + "\n" + e, "OK");
                            }
                            finally
                            {
                                con.Close();
                            }
                        }
                    }
                    if (i == 0)
                    {
                        await DisplayAlert("ALERT", "Incorrect Password or Account Doesn't Exist", "OK");
                    }
                }
            }
        }
    }
}
