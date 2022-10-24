
namespace CommunityEventsMAUI.ViewModels
{
    [QueryProperty(nameof(Events),"Events")]

    public partial class EventInfoPageModel : BaseViewModel
    {

        IMap map;
        public EventInfoPageModel(IMap map)
        {
            this.map = map;
        }

        [ObservableProperty]
        Events events;

        [RelayCommand]
        async Task OpenMap()
        {
            try
            {
                await map.OpenAsync(events.Latitude, events.Longitude, new MapLaunchOptions
                {
                    Name = events.Name,
                    NavigationMode = NavigationMode.None
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
                await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
            }
        }
    }
}
