using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityEventsMAUI.Models;
using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class HomePageModel : BaseViewModel
    {
        //Declares services to access its functions
        public EventService eventService;

        //Creates Dictionaries and other variables to be used
        public Dictionary<string, Events> _Events { get; set; } = new Dictionary<string, Events>();

        public Dictionary<string, Events> _Favorites { get; set; } = new Dictionary<string, Events>();

        public ObservableCollection<Events> eventsList { get; } = new ObservableCollection<Events>();

        public HomePageModel(EventService eventService)
        {
            Title = "Events";
            this.eventService = eventService;
        }

        //Gets the data from the current event and sends it to the details page, while opening the details page
        [RelayCommand]
        async Task GoToDetailsAsync(Events events)
        {
            if (events == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(EventInfoPage)}", true, new Dictionary<string, object>
            {
                {"Events", events }
            });
        }

        //Gets all of the users favorited events from database and fills in the data template of the Favorites events page
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
                var _favorites = await eventService.GetFavorites();
                var _events = await eventService.GetEvents();

                if (_Events != null)
                {
                    _Events.Clear();
                }

                if (_Favorites != null)
                {
                    _Favorites.Clear();
                }

                if (eventsList != null)
                {
                    eventsList.Clear();
                }

                _Events = _events;
                _Favorites = _favorites;

                foreach (var item in _Favorites)
                {
                    try
                    {
                        Events events = _Events[item.Key];
                        eventsList.Add(events);
                    }
                    catch(Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
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
                var _favorites = await eventService.GetFavorites();
                var _events = await eventService.GetEvents();

                if (_Events != null)
                {
                    _Events.Clear();
                }

                if (_Favorites != null)
                {
                    _Favorites.Clear();
                }

                if (eventsList != null)
                {
                    eventsList.Clear();
                }

                _Events = _events;
                _Favorites = _favorites;

                foreach (var item in _Favorites)
                {
                    try
                    {
                        Events events = _Events[item.Key];
                        eventsList.Add(events);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex);
                    }
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
