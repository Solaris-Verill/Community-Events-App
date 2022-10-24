using CommunityEventsMAUI.ViewModels;

namespace CommunityEventsMAUI;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
		Routing.RegisterRoute(nameof(EventInfoPage), typeof(EventInfoPage));
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
	}
}
