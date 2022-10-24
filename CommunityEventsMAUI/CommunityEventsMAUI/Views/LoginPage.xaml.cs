using CommunityEventsMAUI.ViewModels;

namespace CommunityEventsMAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}