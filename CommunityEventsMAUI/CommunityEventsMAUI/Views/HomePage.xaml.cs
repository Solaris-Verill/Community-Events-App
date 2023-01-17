using Newtonsoft.Json;

namespace CommunityEventsMAUI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class HomePage : ContentPage
{
	public HomePage(HomePageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }

    /*private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        HomePageModel viewModel = (HomePageModel)BindingContext;
        await viewModel.GetEvents();
        PageNavigatedFrom.navigatedFrom = "HomePage";
        Trace.WriteLine(PageNavigatedFrom.navigatedFrom);
    }*/
}