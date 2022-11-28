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
}