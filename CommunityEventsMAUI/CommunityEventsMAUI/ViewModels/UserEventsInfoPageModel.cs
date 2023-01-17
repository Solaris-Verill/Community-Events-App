using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp;
using FireSharp.Config;

namespace CommunityEventsMAUI.ViewModels
{
    //Recieves the data that was sent from the user event page
    [QueryProperty(nameof(Events), "Events")]

    public partial class UserEventsInfoPageModel : BaseViewModel
    {
        //Unused - Map needs to be added
        IMap map;
        public UserEventsInfoPageModel(IMap map)
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

        //If the delete button is clicked on an event, askes the user to prevent unwanted deletion, and will either cancel the deletion if no is selected
        [RelayCommand]
        async void DeleteEventAsync()
        {
            ConnectToFirebase();
            try
            {
                bool answer = await Shell.Current.DisplayAlert("Delete", "Do you want to delete this event?", "Yes", "No");
                if (answer == true)
                {
                    await client.DeleteAsync($"User/{Auth.Userid}/Events/{events.EventNumb}");
                    await client.DeleteAsync($"Events/{events.EventNumb}");
                }
                Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
            }
            catch
            {
                await Shell.Current.DisplayAlert("error", "please Try again", "OK");
            }
        }

        //Gets the data from the current event and sends it to the editing page, while opening the editing page
        [RelayCommand]
        async void EditEventAsync(Events Event)
        {
            try
            {
                Event = events;
                ConnectToFirebase();
                if (events == null)
                {
                    return;
                }

                await Shell.Current.GoToAsync($"{nameof(EditEventPage)}", true, new Dictionary<string, object>
                {
                    {"Events", Event }
                });
            }
            catch (Exception ex )
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
