using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FireSharp.Config;
using Firebase.Storage;
using FireSharp;
using FireSharp.Interfaces;
using GoogleMaps.LocationServices;
using FireSharp.Response;

namespace CommunityEventsMAUI.ViewModels
{
    public partial class EventCreationPageModel : Auth
    {
        //Declaration of all user inputs for event
        private string details = "";
        private string title = "";
        private string address = "";
        private string city = "";
        private string state = "";
        private string startTime = "";
        private string endTime = "";
        private string date = "";

        //Creates variables that the inputs of the page can get binded to
        public string Details { get => details; set => details = value; }
        public string Title { get => title; set => title = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }
        public string Date { get => date; set => date = value; }

        //Other variable declarations
        public FileResult result;
        public string downloadUrl;
        public string location;
        static public string GUiD;

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

        //Allows a user to pick a image file from their device and upload it using firebase storage
        [RelayCommand]
        public async Task<FileResult> PickAndShow(PickOptions options)
        {
            GetEventNumb();

            try
            {
                result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    var task = new FirebaseStorage("communityevents-128b1.appspot.com",
                        new FirebaseStorageOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(Token),
                            ThrowOnCancel = true
                        })
                        .Child(GUiD)
                        .Child(result.FileName)
                        .PutAsync(await result.OpenReadAsync());

                    var downloadlink = await task;
                    downloadUrl = downloadlink;
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                await Shell.Current.DisplayAlert("Error", $"{ex}", "OK");
            }

            return result;
        }

        //Created a unique number for the event and image to prevent duplicate identifiers
        private void GetEventNumb()
        {
            GUiD = DateTime.Now.Second.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Year.ToString() + Guid.NewGuid().ToString().GetHashCode().ToString("x");
        }

        //Uploads event to the database
        [RelayCommand]
        async void Upload()
        {
            try
            {
                //Checks to see if there is any missing data and alerts the user if there is any missing data
                if (Title != "" || Details != "" || startTime != "" || Date != "" || State != "" || City != "" || Address != "")
                {
                    //Runs to make the firebase client
                    ConnectToFirebase();

                    //Create a unique id for the created event
                    GetEventNumb();

                    //Sets the image to a grey square if no image is added
                    if (downloadUrl == null)
                    {
                        downloadUrl = "https://firebasestorage.googleapis.com/v0/b/communityevents-128b1.appspot.com/o/grey.png?alt=media&token=411d50c5-d5f6-4b19-8a76-eb41e301c52f";
                    }
                
                    try
                    {
                        //Creates the updated event with the data that was added
                        Events events = new Events()
                        {
                            Details = Details,
                            Image = downloadUrl,
                            Name = Title, 
                            Address = Address,
                            City = City,
                            State = State,
                            Date = Date,
                            StartTime = StartTime,
                            EndTime = EndTime,
                            EventNumb = GUiD
                        };


                        //Creates a variable that allows the user to access the event on the user events page
                        Events guid = new Events()
                        {
                            EventNumb = GUiD
                        };

                        //updates the data in the database
                        await client.SetAsync($"User/{Userid}/Events/{GUiD}", guid);
                        await client.SetAsync($"Events/{GUiD}", events);
                    }
                    finally
                    {
                        //Goes back to the user events page
                        Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Missing Info", "There is missing information for this event. Please enter data and retry", "OK");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.ToString(), "OK");
            }
            
        }
    }
}
