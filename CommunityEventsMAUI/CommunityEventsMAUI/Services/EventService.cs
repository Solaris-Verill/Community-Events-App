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

        List<Events> eventList = new();

        public async Task<List<Events>> GetEvents()
        {
            ConnectToFirebase();

            if (eventList?.Count > 0)
            {
                eventList = new List<Events>();
            }
            FirebaseResponse response = await client.GetAsync(@"Events");
            Events[] result = response.ResultAs<Events[]>();

            foreach (var events in result)
            {
                eventList.Add(events);
            }


            return await Task.FromResult(eventList);
        }

        /*
                //MAUI Tutorial
                HttpClient httpClient;
                public EventService()
                {
                    httpClient = new HttpClient();
                }

                List<Events> eventList = new List<Events>();

                public async Task<List<Events>> GetEvents()
                {
                    if (eventList?.Count > 0)
                    {
                        return eventList;
                    }

                    var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
                    var response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        eventList = await response.Content.ReadFromJsonAsync<List<Events>>();
                    }

                    return null;
                }*/
    }
}
