using CommunityEventsMAUI.Models;
using CommunityEventsMAUI.Services;
using CommunityEventsMAUI.ViewModels;
using CommunityEventsMAUI.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Debug = System.Diagnostics.Debug;
namespace CommunityEventsMAUI.ViewModels
{


    public partial class BaseViewModel : ObservableObject
    {

        public BaseViewModel()
        {
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => !IsBusy;
    }
}
