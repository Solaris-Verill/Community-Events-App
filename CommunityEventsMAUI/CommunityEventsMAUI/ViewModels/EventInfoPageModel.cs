using CommunityEventsMAUI.Models;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Linq;

namespace CommunityEventsMAUI.ViewModels
{
    [QueryProperty(nameof(Events), "Events")]

    public partial class EventInfoPageModel : BaseViewModel
    {
        //Unused - Map needs to be added
        IMap map;
        public EventInfoPageModel(IMap map)
        {
            this.map = map;
        }

        //used to access event data
        [ObservableProperty]
        Events events;

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

        [RelayCommand]
        async void Favorite()
        {            
            try 
            {
                ConnectToFirebase();

                if (Users.Favorites != null)
                {
                    Events guid = new Events()
                    {
                        EventNumb = events.EventNumb
                    };

                    await client.SetAsync($"User/{Auth.Userid}/Favorites/{events.EventNumb}", guid);
                }
                else
                {
                    Users.Favorites = new();
                    Events guid = new Events()
                    {
                        EventNumb = events.EventNumb
                    };

                    await client.SetAsync($"User/{Auth.Userid}/Favorites/{events.EventNumb}", guid);
                }
            }
            catch ( Exception ex)
            {
                Shell.Current.DisplayAlert("error", ex.ToString(), "OK");
            }
        }

        [RelayCommand]
        async void Unfavorite()
        {
            try
            {
                ConnectToFirebase();
                bool answer = await Shell.Current.DisplayAlert("Delete", "Do you want to Unfavorite this event?", "OK", "Cancel");
                if (answer == true)
                {
                    await client.DeleteAsync($"User/{Auth.Userid}/Favorites/{events.EventNumb}");
                }
                Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("error", ex.ToString(), "OK");
            }
        }
    }
}
