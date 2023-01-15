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
    public partial class AddUserToDB : Auth
    {

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

        //Adds the user to firebase
        public async Task<bool> AddUser(string email, string uid)
        {
            try
            {
                ConnectToFirebase();
                Users user = new Users()
                {
                    Username = email,
                    Role = "User" 
                };

                await client.SetAsync($"User/{uid}/", user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
