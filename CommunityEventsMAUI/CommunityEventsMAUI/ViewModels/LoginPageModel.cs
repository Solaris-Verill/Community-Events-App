using CommunityEventsMAUI.Views;
using Firebase.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class LoginPageModel
    {
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";
        private string userName;
        private string userPassword;

        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName
        {
            get => userName; 
            
            set
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
        }

        public string UserPassword
        {
            get => userPassword; 
            
            set
            {
                userPassword = value;
                RaisePropertyChanged("UserPassword");
            }
        }

        [RelayCommand]
        private async void LoginBtnTappedAsync(object obj)
        {
            if(!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserPassword))
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                try
                {
                    userName = userName.ToLower();
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserName, UserPassword);
                    var content = await auth.GetFreshAuthAsync();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                catch
                {
                    await Shell.Current.DisplayAlert("Error!", $"Invalid Email Or Password", "OK");
                }
            }


        }

        [RelayCommand]
        async Task RegisterBtnTappedAsync(Events events)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    }
}