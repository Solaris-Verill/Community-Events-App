using CommunityEventsMAUI.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Extensions;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using CommunityEventsMAUI.Services;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CommunityEventsMAUI.Services
{
    public class EventService
    {
        //Firebase.Net Attempt
        /*public async Task<List<Events>> GetEvents()
        {
            var firebase = new FirebaseClient("https://communityevents-128b1-default-rtdb.firebaseio.com/");
            var _Events = await firebase
                .Child("Events")
                .OrderByKey()
                .LimitToFirst(2)
                .OnceAsync<Events>();

            return null;
        }*/


        //FireSharp Attempt

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

        //Declaration Of Dictionaries
        Dictionary<string, Events> eventsList = new();
        Dictionary<string, Events> usereventsList = new();
        Dictionary<string, Events> favoritesList = new();

        //Gets all events for the event page, and all the events for admins in the user events page
        public async Task<Dictionary<string, Events>> GetEvents()
        {
            ConnectToFirebase();

            if (eventsList?.Count > 0)
            {
                eventsList = new Dictionary<string, Events>();
            }
            FirebaseResponse response = await client.GetAsync(@"Events");
            Dictionary<string, Events> result = response.ResultAs<Dictionary<string, Events>>();

            eventsList = result;

            return await Task.FromResult(eventsList);
        }
        
        //Gets all the users events for the user events page
        public async Task<Dictionary<string, Events>> GetUserEvents()
        {
            ConnectToFirebase();

            if (usereventsList?.Count > 0)
            {
                usereventsList = new Dictionary<string, Events>();
            }

            try
            {
                FirebaseResponse response = await client.GetAsync(@$"User/{Auth.Userid}/Events");
                Dictionary<string, Events> result = response.ResultAs<Dictionary<string, Events>>();

                usereventsList = result;

                return await Task.FromResult(usereventsList);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error!", "No Events Found", "OK");

                return await Task.FromResult(usereventsList);
            }
        }

        //Gets all the users favorites for the home page
        public async Task<Dictionary<string, Events>> GetFavorites()
        {
            ConnectToFirebase();

            if (favoritesList?.Count > 0)
            {
                favoritesList = new Dictionary<string, Events>();
            }

            try
            {
                FirebaseResponse response = await client.GetAsync(@$"User/{Auth.Userid}/Favorites");

                Dictionary<string, Events> result = response.ResultAs<Dictionary<string, Events>>();

                favoritesList = result;

                return await Task.FromResult(favoritesList);
            }
            catch
            {
                await Shell.Current.DisplayAlert("Error!", "No Events Found", "OK");

                return await Task.FromResult(favoritesList);
            }
        }
    }
}
