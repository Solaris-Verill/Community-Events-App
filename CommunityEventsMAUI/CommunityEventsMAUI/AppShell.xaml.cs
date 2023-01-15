using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.ViewModels;
using CommunityEventsMAUI.Views;

namespace CommunityEventsMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
		Routing.RegisterRoute(nameof(EventCreationPage), typeof(EventCreationPage));
		Routing.RegisterRoute(nameof(EventInfoPage), typeof(EventInfoPage));
        Routing.RegisterRoute(nameof(EventPage), typeof(EventPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
		Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
		Routing.RegisterRoute(nameof(UserEventInfo), typeof(UserEventInfo));
		Routing.RegisterRoute(nameof(UserEvents), typeof(UserEvents)); 
		Routing.RegisterRoute(nameof(EditEventPage), typeof(EditEventPage));
		Routing.RegisterRoute(nameof(AdminRegistrationPage), typeof(AdminRegistrationPage));
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		await LogOut.logOutAsync();
	}
}
