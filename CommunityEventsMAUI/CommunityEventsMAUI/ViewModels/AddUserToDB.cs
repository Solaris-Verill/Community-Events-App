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
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";

        public IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "Inule8rXhgsMUGuNGJw6zIoJaEdHpuIYZjeDo9BY",
            BasePath = "https://communityevents-128b1-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        public void ConnectToFirebase()
        {
            try
            {
                client = new FirebaseClient(ifc);
            }
            catch
            {
                Shell.Current.DisplayAlert("Error!", "Unable to Connect. Try Again Later", "OK");
            }
        }

        List<Events> favoritesList = new();
        List<Events> Events = new();

        public async void AddUser(string email, string uid)
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
            }
            finally
            {

            }
        }
    }
}
