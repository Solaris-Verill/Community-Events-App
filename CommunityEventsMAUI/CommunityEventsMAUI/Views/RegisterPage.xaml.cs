namespace CommunityEventsMAUI.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
	}

	public RegisterPage(RegisterPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}
}