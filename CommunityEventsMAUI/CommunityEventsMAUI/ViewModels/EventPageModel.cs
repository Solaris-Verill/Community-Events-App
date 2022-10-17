using CommunityEventsMAUI.Services;

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

                if(_Events.Count != 0)
                {
                    _Events.Clear();
                }

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
    }
}
