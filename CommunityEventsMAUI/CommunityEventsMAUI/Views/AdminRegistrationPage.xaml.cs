namespace CommunityEventsMAUI.Views;

public partial class AdminRegistrationPage : ContentPage
{
	public AdminRegistrationPage(AdminRegistrationPageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}