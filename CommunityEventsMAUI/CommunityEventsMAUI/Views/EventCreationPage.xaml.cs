namespace CommunityEventsMAUI.Views;

public partial class EventCreationPage : ContentPage
{
	public EventCreationPage(EventCreationPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}