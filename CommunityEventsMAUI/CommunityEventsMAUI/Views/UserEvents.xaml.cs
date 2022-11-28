

namespace CommunityEventsMAUI.Views;

public partial class UserEvents : ContentPage
{
	public UserEvents(UserEventsPageModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        UserEventsPageModel viewModel = (UserEventsPageModel)BindingContext;
        await viewModel.GetEvents();
    }
}