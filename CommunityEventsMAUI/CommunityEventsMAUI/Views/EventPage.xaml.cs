
using CommunityEventsMAUI.Services;

namespace CommunityEventsMAUI.Views;

public partial class EventPage : ContentPage
{
    public EventPage(EventPageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		EventPageModel viewModel = (EventPageModel)BindingContext;
		await viewModel.GetEvents();
		PageNavigatedFrom.navigatedFrom = "EventPage";
		Trace.WriteLine(PageNavigatedFrom.navigatedFrom);
	}
}