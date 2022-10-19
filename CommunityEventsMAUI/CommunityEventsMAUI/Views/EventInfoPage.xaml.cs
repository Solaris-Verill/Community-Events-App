namespace CommunityEventsMAUI.Views;

public partial class EventInfoPage : ContentPage
{
	public EventInfoPage(EventInfoPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);
	}
}