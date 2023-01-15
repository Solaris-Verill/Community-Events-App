using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.ViewModels;
using CommunityEventsMAUI.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Devices;
using Microsoft.Maui.LifecycleEvents;

namespace CommunityEventsMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{


		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
        builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
        builder.Services.AddSingleton<IMap>(Map.Default);

		builder.Services.AddSingleton<EventService>();

        builder.Services.AddSingleton<EventCreationPage>();
		builder.Services.AddTransient<EventInfoPage>();
		builder.Services.AddSingleton<EventPage>();
		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddSingleton<ProfilePage>();
		builder.Services.AddTransient<RegisterPage>();
		builder.Services.AddSingleton<SettingsPage>();
		builder.Services.AddTransient<UserEventInfo>();
		builder.Services.AddSingleton<UserEvents>();
		builder.Services.AddTransient<EditEventPage>();
		builder.Services.AddTransient<AdminRegistrationPage>();

		builder.Services.AddTransient<HomePageModel>();
        builder.Services.AddTransient<EventCreationPageModel>();
        builder.Services.AddTransient<EventInfoPageModel>();
        builder.Services.AddTransient<EventPageModel>();
        builder.Services.AddTransient<LoginPageModel>();
        builder.Services.AddTransient<ProfilePageModel>();
		builder.Services.AddTransient<RegisterPageModel>();
        builder.Services.AddTransient<SettingsPageModel>();
		builder.Services.AddTransient<UserEventsPageModel>();
		builder.Services.AddTransient<UserEventsInfoPageModel>();
		builder.Services.AddTransient<EditEventPageModel>();
		builder.Services.AddTransient<AdminRegistrationPageModel>();

        return builder.Build();

	}
}
// a person with experience is never at the mercy to a person as a theory - Mentor of Sekol's