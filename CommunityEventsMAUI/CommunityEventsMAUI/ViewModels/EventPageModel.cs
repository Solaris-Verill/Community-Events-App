using CommunityEventsMAUI.Models;
using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class EventPageModel : BaseViewModel
    {
        public ObservableCollection<Events> _Events { get; } = new();
        EventService eventService;
        IConnectivity connectivity;
        IGeolocation geolocation;
        public EventPageModel(EventService eventService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            this.eventService = eventService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [RelayCommand]
        async Task GoToDetails(Events _event)
        {
            if (_event == null)
                return;

            await Shell.Current.GoToAsync(nameof(EventInfoPage), true, new Dictionary<string, object>
            {
                {"Event", _event }
            });
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand]
        async Task GetEventsAsync()
        {
            Debug.WriteLine("Send to debug output.");
            if (IsBusy)
                return;

            try
            {
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }

                IsBusy = true;
                var monkeys = await eventService.GetEvents();

                if (_Events.Count != 0)
                    _Events.Clear();

                foreach (var monkey in monkeys)
                    _Events.Add(monkey);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }

        }

        [RelayCommand]
        async Task GetClosestEvent()
        {
            Debug.WriteLine("Send to debug output.");
            if (IsBusy || _Events.Count == 0)
                return;

            try
            {
                // Get cached location, else get real location.
                var location = await geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                // Find closest monkey to us
                var first = _Events.OrderBy(m => location.CalculateDistance(
                    new Location(m.Latitude, m.Longitude), DistanceUnits.Miles))
                    .FirstOrDefault();

                await Shell.Current.DisplayAlert("", first.Name + " " +
                    first.Location, "OK");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to query location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
