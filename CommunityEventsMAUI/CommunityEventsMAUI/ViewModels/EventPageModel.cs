using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.Views;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class EventPageModel : BaseViewModel
    {
        public EventService eventService;

        public ObservableCollection<Events> _Events { get; } = new ObservableCollection<Events>();

        public EventPageModel(EventService eventService)
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

            await Shell.Current.GoToAsync($"{nameof(EventInfoPage)}" , true, new Dictionary<string, object>
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
                var _events = await eventService.GetEvents();

                _Events.Clear();

                foreach(var _event in _events)
                {
                    _Events.Add(_event);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
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
                var _events = await eventService.GetEvents();

                _Events.Clear();

                foreach (var _event in _events)
                {
                    _Events.Add(_event);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
