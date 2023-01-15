using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FireSharp.Interfaces;
using FireSharp;

namespace CommunityEventsMAUI.ViewModels
{
    public class AddAdminToDB : Auth
    {
        //Declaration of local variables
        static string adminIDList;

        //Firebase Configuration so that the app can connect to the Firebase server
        public IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "Inule8rXhgsMUGuNGJw6zIoJaEdHpuIYZjeDo9BY",
            BasePath = "https://communityevents-128b1-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        //Trys to connect and sets the firebase client to be used
        public void ConnectToFirebase()
        {
            try
            {
                client = new FirebaseClient(ifc);
            }
            catch
            {
                Shell.Current.DisplayAlert("Error!", "Unable To Connect. Try Again Later", "OK");
            }
        }

        //Adds an admin to the firebase
        public async Task<bool> AddAdmin(string email, string uid, string adminNumb)
        {
            try
            {
                await GetAdminIDs();
                bool isAdmin = IsAdmin(adminNumb);

                //Creates the admin account if isAdmin and returns true, or returns false otherwise
                if (isAdmin == true)
                {
                    Users user = new Users()
                    {
                        Username = email,
                        Role = "Admin"
                    };

                    await client.SetAsync($"User/{uid}/", user);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //Compares the admin id from the database with the users input to determine if the user put the correct ID in
        public static bool IsAdmin(string adminID)
        {
            if (adminID == null)
            {
                return false;
            }
            else if (adminID == adminIDList)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
        //Gets the Admin ID from the database - Need to make a way to change this variable
        public async Task GetAdminIDs()
        {
            ConnectToFirebase();

            if (adminIDList != "")
            {
                adminIDList = "";
            }

            try
            {
                FirebaseResponse response = await client.GetAsync(@"Admin/1/");
                string result = response.ResultAs<string>();

                adminIDList = result;
            }
            catch(Exception ex)
            {
                //Shell.Current.DisplayAlert("Error!", ex.ToString(), "OK");
                //Shell.Current.DisplayAlert("Error!", "No AdminID's Found", "OK");
            }
        }
    }
}

