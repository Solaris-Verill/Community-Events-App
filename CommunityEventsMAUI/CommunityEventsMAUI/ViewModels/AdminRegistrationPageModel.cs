using Firebase.Auth;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class AdminRegistrationPageModel : AddAdminToDB
    {
        //Variable Declaration
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";
        private string email;
        private string password;
        private string adminNumber;
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

        public string AdminNumber
        {
            get => adminNumber; set
            {
                adminNumber = value;
                RaisePropertyChanged("AdminNumber");
            }
        }

        private void RaisePropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }

        //Registers the Admin
        [RelayCommand]
        private async void RegisterAdminTappedAsync(object obj)
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
                    //Runs the AddAdmin in the AddAdminToDB class and returns if the admin was successfully added to the database
                    bool adminAdded = await AddAdmin(Email, Uid, AdminNumber);

                    //If the admin is not created it deletes the email form the Firebase authentication so that the user can try again
                    if (!adminAdded)
                    {
                        await authProvider.DeleteUserAsync(token);
                        await App.Current.MainPage.DisplayAlert("Alert", "There had been an error that has occured. Please make sure all information is correct and try again.", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Admin Registered successfully", "OK");
                        Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
                    }
                    
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "There had been an error that has occured. Please make sure all information is correct and try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "There had been an error that has occured. Please make sure all information is correct and try again.", "OK");
            }
        }
    }
}