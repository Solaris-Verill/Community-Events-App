using CommunityEventsMAUI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class UserEventsPageModel : BaseViewModel
    {
        public UserEventService eventService;

        public ObservableCollection<Events> _Events { get; } = new ObservableCollection<Events>();

        public event PropertyChangedEventHandler PropertyChanged;

        public UserEventsPageModel(UserEventService eventService)
        {
            Title = "Events";
            this.eventService = eventService;
        }

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
                var _events = await eventService.GetPeople();

                if (_Events.Count != 0)
                {
                    _Events.Clear();
                }

                foreach (var _event in _events)
                {
                    _Events.Add(_event);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task CreateEventBtnTappedAsync(Events events)
        {
            await Shell.Current.GoToAsync($"{nameof(EventCreationPage)}");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        public async Task GetEvents()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var _events = await eventService.GetPeople();

                if (_Events.Count != 0)
                {
                    _Events.Clear();
                }

                foreach (var _event in _events)
                {
                    _Events.Add(_event);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", $"Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
