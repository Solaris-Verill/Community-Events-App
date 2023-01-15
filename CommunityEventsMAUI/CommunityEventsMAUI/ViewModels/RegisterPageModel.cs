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
        //Variable Declaration
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";
        private string email;
        private string password;
        private string Uid;
        public event PropertyChangedEventHandler PropertyChanged;

        //Creates bindable data for inputs on the admin registration page.
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

        //Registers the user
        [RelayCommand]
        private async void RegisterUserTappedAsync(object obj)
        {
            try
            {
                email = email.ToLower();

                //Creates the User in Firebase's authentication
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(webApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(Email, Password);
                string token = auth.FirebaseToken;
                Uid = auth.User.LocalId;

                //Checks to make sure that the token was created and the user is registered
                if (token != null)
                {
                    bool userAdded = await AddUser(Email, Uid);

                    if (!userAdded)
                    {
                        await authProvider.DeleteUserAsync(token);
                        await App.Current.MainPage.DisplayAlert("Alert", "There had been an error that has occured. Please make sure all information is correct and try again.", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "User Registered successfully", "OK");
                        Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
                    }

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "There had been an error that has occured. Please make sure all information is correct and try again.", "OK");
            }
        }

        //Brings user to the admin registration page
        [RelayCommand]
        async Task AdminRegisterbtn()
        {
            Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
            await Shell.Current.GoToAsync($"{nameof(AdminRegistrationPage)}");
        }
    }
}