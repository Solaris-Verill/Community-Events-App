namespace CommunityEventsMAUI.Views;

public partial class UserEventInfo : ContentPage
{
	public UserEventInfo(UserEventsInfoPageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}