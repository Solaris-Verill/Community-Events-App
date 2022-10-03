using CommunityEvents.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CommunityEvents.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}