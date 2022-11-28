using CommunityEventsMAUI.Models;
using Firebase.Auth;
using FirebaseAdmin.Auth;
using FireSharp;
using FireSharp.Config;
using FireSharp.Extensions;
using FireSharp.Interfaces;
using FireSharp.Response;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirebaseConfig = FireSharp.Config.FirebaseConfig;

namespace CommunityEventsMAUI.Services
{
    public class UserEventService
    {
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
                Shell.Current.DisplayAlert("Error!", "Unable to Access Events1", "OK");
            }
        }

        public Users Users { get; set; }

        List<Events> eventsList = new();

        public async Task<List<Events>> GetPeople()
        {
            ConnectToFirebase();

            if (eventsList?.Count > 0)
            {
                eventsList = new List<Events>();
            }

            try
            {
                FirebaseResponse response = await client.GetAsync(@$"User/{Auth.Userid}/Events");
                Events[] result = response.ResultAs<Events[]>();

                foreach (var people in result)
                {
                    eventsList.Add(people);
                }

                return await Task.FromResult(eventsList);
            }
            catch
            {
                Shell.Current.DisplayAlert("Error!", "No Events Found", "OK");

                return await Task.FromResult(eventsList);
            }
        }
    }
}
