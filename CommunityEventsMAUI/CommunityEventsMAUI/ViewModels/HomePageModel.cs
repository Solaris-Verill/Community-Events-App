using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityEventsMAUI.Models;
using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Debug = System.Diagnostics.Debug;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class HomePageModel : BaseViewModel
    {
        public FavoriteService favoriteService;

        public ObservableCollection<Events> _Events { get; } = new ObservableCollection<Events>();

        public HomePageModel(FavoriteService favoriteService)
        {
            Title = "Events";
            this.favoriteService = favoriteService;
        }

        [RelayCommand]
        async Task GoToDetailsAsync(Events events)
        {
            if (events == null)
            {
                return;
            }

            await Shell.Current.GoToAsync($"{nameof(EventInfoPage)}", true, new Dictionary<string, object>
            {
                {"Events", events }
            });
        }

        [RelayCommand]
        async Task GetEventsAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;
                var _events = await favoriteService.GetPeople();

                _Events.Clear();

                foreach (var _event in _events)
                {
                    _Events.Add(_event);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error!", "Unable to Access Events", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
