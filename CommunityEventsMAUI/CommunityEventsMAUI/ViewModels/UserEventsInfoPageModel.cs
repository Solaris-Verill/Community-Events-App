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
    [QueryProperty(nameof(Events), "Events")]

    public partial class UserEventsInfoPageModel : BaseViewModel
    {

        IMap map;
        public UserEventsInfoPageModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        Events events;

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
        async void DeleteEventAsync()
        {
            ConnectToFirebase();
            try
            {

                await client.DeleteAsync($"User/{Auth.Userid}/Favorites/{Events.EventNumb}");
            }
            catch
            {
                Shell.Current.DisplayAlert("error", "please Try again", "OK");
            }
        }
    }
}
