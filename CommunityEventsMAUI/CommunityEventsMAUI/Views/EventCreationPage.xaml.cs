
namespace CommunityEventsMAUI.Views;

public partial class EventCreationPage : ContentPage
{
	public EventCreationPage(EventCreationPageModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
        try
        {
            if (TitleEntry.Text != "" || DetailsEntry.Text != "" || StartTimeEntry.Text != "" || EndTimeEntry.Text != "" || DateEntry.Text != "" || StateEntry.Text != "" || CityEntry.Text != "" || AddressEntry.Text != "")
            {
                bool userAnswer = await Shell.Current.DisplayAlert("Restore", "Do you want to restore previous event creation?", "Yes", "No");
                if (!userAnswer)
                {
                    TitleEntry.Text = "";
                    DetailsEntry.Text = "";
                    AddressEntry.Text = "";
                    CityEntry.Text = "";
                    StateEntry.Text = "";
                    StartTimeEntry.Text = "";
                    EndTimeEntry.Text = "";
                    DateEntry.Text = "";
                }
            }
        }
        catch
        {

        }
    }
}