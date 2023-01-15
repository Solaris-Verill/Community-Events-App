using CommunityEventsMAUI.Services;

namespace CommunityEventsMAUI.Views;

public partial class EventInfoPage : ContentPage
{
	public EventInfoPage(EventInfoPageModel viewmodel)
	{
		InitializeComponent();
		//Using MVVM to bind the different fields in the interface with the respective view model
		BindingContext = viewmodel;

		//Changes the button command and text based on the page that it nagivated from
        if (PageNavigatedFrom.navigatedFrom == "HomePage")
        {
			btnFavorite.Text = "Unfavorite";
			btnFavorite.Command = viewmodel.UnfavoriteCommand;
        }
        else if (PageNavigatedFrom.navigatedFrom == "EventPage")
        {
            btnFavorite.Text = "Favorite";
            btnFavorite.Command = viewmodel.FavoriteCommand;
        }
    }

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}