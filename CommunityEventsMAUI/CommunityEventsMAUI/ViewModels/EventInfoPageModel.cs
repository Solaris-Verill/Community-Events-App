using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;

namespace CommunityEventsMAUI.ViewModels
{
    [QueryProperty(nameof(Events), "Events")]

    public partial class EventInfoPageModel : BaseViewModel
    {
        public Events selectedEvent;

        IMap map;
        public EventInfoPageModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        Events events;

        [ObservableProperty]
        Users user;

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
                Shell.Current.DisplayAlert("Error!", "Unable To Connect. Try Again Later", "OK");
            }
        }

        [RelayCommand]
        async void Favorite()
        {
            ConnectToFirebase();

            try 
            {
                Users.Favorites.Add(new Favorites { favoriteNumber = events.EventNumb });

                await client.UpdateAsync($"User/{Auth.Userid}/Favorites", Users.Favorites);
            }
            catch
            {
                Shell.Current.DisplayAlert("error", "please Try again", "OK");
            }
        }
    }
}
