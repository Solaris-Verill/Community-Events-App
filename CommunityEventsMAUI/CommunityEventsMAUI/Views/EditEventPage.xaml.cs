namespace CommunityEventsMAUI.Views;

public partial class EditEventPage : ContentPage
{
	public EditEventPage(EditEventPageModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}