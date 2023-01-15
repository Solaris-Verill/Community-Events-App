using CommunityEventsMAUI.ViewModels;

namespace CommunityEventsMAUI.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

	private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		etyUserName.Text = "";
		etyPassword.Text = "";
	}
}