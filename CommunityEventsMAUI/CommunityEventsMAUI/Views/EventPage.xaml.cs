namespace CommunityEventsMAUI.Views;

public partial class EventPage : ContentPage
{
	public EventPage(EventPageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}