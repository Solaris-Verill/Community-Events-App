
namespace CommunityEventsMAUI.ViewModels
{
    [QueryProperty("Events","Events")]

    public partial class EventInfoPageModel : BaseViewModel
    {
        public EventInfoPageModel()
        {

        }

        [ObservableProperty]
        Events events; 
    }
}
