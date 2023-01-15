using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.Views;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using Microsoft.Extensions.Logging;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class EventPageModel : BaseViewModel
    {
        public EventPageModel(EventService eventService)
        {
            Title = "Events";
            this.eventService = eventService;
        }

        //Declares services to access its functions
        public EventService eventService;

        //Creates Dictionaries and other variables to be used
        public static Dictionary<string, Events> _Events { get; set; } = new Dictionary<string, Events>();

        public ObservableCollection<Events> eventsList { get; } = new ObservableCollection<Events>();

        public event PropertyChangedEventHandler PropertyChanged;

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

        //Gets the data from the current event and sends it to the details page, while opening the details page
        [RelayCommand]
        async Task GoToDetailsAsync(Events events)
        {
            if (events == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(EventInfoPage)}" , true, new Dictionary<string, object>
            {
                {"Events", events }
            });
        }

        //Gets all of the events from database and fills in the data template of the events page
        [RelayCommand]
        async Task GetEventsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var _events = await eventService.GetEvents();

                if(_Events != null)
                {
                    _Events.Clear();
                }
                
                if(eventsList != null)
                {
                    eventsList.Clear();
                }

                _Events = _events;

                foreach (var item in _Events.Values)
                {
                    eventsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        //Funtion that is called when the page opened - acts the same as its relay command counterpart
        public async Task GetEvents()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var _events = await eventService.GetEvents();

                if (_Events != null)
                {
                    _Events.Clear();
                }

                if (eventsList != null)
                {
                    eventsList.Clear();
                }

                _Events = _events;

                foreach (var item in _Events.Values)
                {
                    eventsList.Add(item);
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
