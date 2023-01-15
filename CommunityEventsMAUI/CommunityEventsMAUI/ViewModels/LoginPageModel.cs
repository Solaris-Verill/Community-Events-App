using CommunityEventsMAUI.Views;
using Firebase.Auth;
using FireSharp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using FireSharp.Response;
using FirebaseConfig = FireSharp.Config.FirebaseConfig;
using FireSharp;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class LoginPageModel : Auth
    {
        public string webApiKey = "AIzaSyBNKQZYQthu5ucaviE21YffjNpDBBT3lII";
        private string userName;
        private string userPassword;
        public string Uid;

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

        //Firebase Configuration so that the app can connect to the Firebase server
        public IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "Inule8rXhgsMUGuNGJw6zIoJaEdHpuIYZjeDo9BY",
            BasePath = "https://communityevents-128b1-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

        //Trys to connect and sets the firebase client to be used
        public void ConnectToFirebase()
        {
            try
            {
                client = new FirebaseClient(ifc);
            }
            catch
            {
                Shell.Current.DisplayAlert("Error!", "Unable To Connect. Try Again Later", "OK");
            }
        }

        //Logs the user in
        [RelayCommand]
        private async void LoginBtnTappedAsync(object obj)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrWhiteSpace(UserPassword))
            {
                try
                {
                    var authProvider = new FirebaseAuthProvider(new Firebase.Auth.FirebaseConfig(webApiKey));
                    userName = userName.ToLower();
                    var auth = await authProvider.SignInWithEmailAndPasswordAsync(UserName, UserPassword);
                    var content = await auth.GetFreshAuthAsync();
                    Uid = auth.User.LocalId;
                    Userid = Uid;
                    Token = auth.FirebaseToken;
                    await GetUserRole();
                    var serializedContent = JsonConvert.SerializeObject(content);
                    Preferences.Set("FreshFirebaseToken", serializedContent);
                    Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Error!", $"Invalid Username or Password.", "OK");

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

        //Gets the users role - Admin or User - and stores it in the Auth Model
        private async Task<string> GetUserRole()
        {
            try
            {
                ConnectToFirebase();
                FirebaseResponse response = await client.GetAsync(@$"User/{Auth.Userid}/Role");
                string result = response.ResultAs<string>();

                Auth.Role = result;

                return result;
            }
            catch
            {
                Shell.Current.DisplayAlert("Error!", "No Role Found. Setting as User", "OK");

                Auth.Role = "User";

                return "User";
            }
        }
    }
}