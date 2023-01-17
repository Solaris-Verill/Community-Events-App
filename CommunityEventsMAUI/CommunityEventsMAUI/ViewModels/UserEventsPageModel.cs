using CommunityEventsMAUI.Services;
using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CommunityEventsMAUI.ViewModels
{
    public partial class UserEventsPageModel : BaseViewModel
    {
        public EventService eventService;

        public Dictionary<string, Events> _Events { get; set; } = new Dictionary<string, Events>();

        public Dictionary<string, Events> _UserEvents { get; set; } = new Dictionary<string, Events>();

        public ObservableCollection<Events> eventsList { get; } = new ObservableCollection<Events>();

        public UserEventsPageModel(EventService eventService)
        {
            Title = "Events";
            this.eventService = eventService;

        }

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

            await Shell.Current.GoToAsync($"{nameof(UserEventInfo)}", true, new Dictionary<string, object>
            {
                {"Events", events }
            });
        }

        //Gets all of the users event data from database and fills in the data template of the user events page (If the user is an admin, it will retrieve all events in the database and will have access to edit and delete any events)
        [RelayCommand]
        async Task GetEventsAsync()
        {
            if (Auth.Role == "Admin")
            {
                if (IsBusy)
                {
                    return;
                }

                try
                {
                    IsBusy = true;
                    

                    if(_Events != null)
                    {
                        _Events.Clear();
                    }

                    if (eventsList != null)
                    {
                        eventsList.Clear();
                    }                    
                    
                    var _events = await eventService.GetEvents();
                    _Events = _events;

                    if (_Events != null)
                    {
                        foreach (var item in _Events.Values)
                        {
                            eventsList.Add(item);
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
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
            else
            {
                if (IsBusy)
                {
                    return;
                }

                try
                {
                    IsBusy = true;
                    var _userEvents = await eventService.GetUserEvents();
                    var _events = await eventService.GetEvents();

                    if (_Events != null)
                    {
                        _Events.Clear();
                    }

                    if (_UserEvents != null)
                    {
                        _UserEvents.Clear();
                    }

                    if (eventsList != null)
                    {
                        eventsList.Clear();
                    }

                    _Events = _events;
                    _UserEvents = _userEvents;

                    if (_Events != null && _userEvents != null)
                    {
                        foreach (var item in _UserEvents)
                        {
                            Events events = _Events[item.Key];
                            eventsList.Add(events);
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
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
        }

        //Brings the user to the event creator allowing them to their own events
        [RelayCommand]
        async Task CreateEventBtnTappedAsync(Events events)
        {
            await Shell.Current.GoToAsync($"{nameof(EventCreationPage)}");
        }

        //Funtion that is called when the page opened - acts the same as its relay command counterpart
        public async Task GetEvents()
        {
            if (Auth.Role == "Admin")
            {
                if (IsBusy)
                {
                    return;
                }

                try
                {
                    IsBusy = true;


                    if (_Events != null)
                    {
                        _Events.Clear();
                    }

                    if (eventsList != null)
                    {
                        eventsList.Clear();
                    }

                    var _events = await eventService.GetEvents();
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
            else
            {
                if (IsBusy)
                {
                    return;
                }

                try
                {
                    IsBusy = true;
                    var _userEvents = await eventService.GetUserEvents();
                    var _events = await eventService.GetEvents();

                    if (_Events != null)
                    {
                        _Events.Clear();
                    }

                    if (_UserEvents != null)
                    {
                        _UserEvents.Clear();
                    }

                    if (eventsList != null)
                    {
                        eventsList.Clear();
                    }

                    _Events = _events;
                    _UserEvents = _userEvents;

                    foreach (var item in _UserEvents)
                    {
                        Events events = _Events[item.Key];
                        eventsList.Add(events);
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
}
