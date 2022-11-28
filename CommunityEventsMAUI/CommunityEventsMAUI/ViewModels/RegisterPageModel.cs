using Firebase.Auth;
using FireSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityEventsMAUI.Models;
using CommunityEventsMAUI.Services;
using FireSharp.Interfaces;
using CommunityToolkit.Mvvm.Input;
using FireSharp.Response;
using Newtonsoft.Json;
using FireSharp.Extensions;
using FireSharp.Exceptions;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class RegisterPageModel : AddUserToDB
    {
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";
        private string email;
        private string password;
        private string Uid;

        public IFirebaseClient client;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Email
        {
            get => email;
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get => password; set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        public Users UID { get; set; }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        [RelayCommand]
        private async void RegisterUserTappedAsync(object obj)
        {
            try
            {
                email = email.ToLower();
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                string token = auth.FirebaseToken;
                Uid = auth.User.LocalId;
                if (token != null)
                    AddUser(Email, Uid);
                    await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
                throw;
            }
        }
    }
}