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
        private string details;
        private string title;
        private string address;
        private string city;
        private string state;
        private string startTime;
        private string endTime;
        private string date;

        public string Details { get => details; set => details = value; }
        public string Title { get => title; set => title = value; }
        public string Address { get => address; set => address = value; }
        public string City { get => city; set => city = value; }
        public string State { get => state; set => state = value; }
        public string StartTime { get => startTime; set => startTime = value; }
        public string EndTime { get => endTime; set => endTime = value; }
        public string Date { get => date; set => date = value; }

        public FileResult result;
        public string downloadUrl;
        public string location;
        public int numEvents;


        public IFirebaseConfig ifc = new FirebaseConfig()
        {
            AuthSecret = "Inule8rXhgsMUGuNGJw6zIoJaEdHpuIYZjeDo9BY",
            BasePath = "https://communityevents-128b1-default-rtdb.firebaseio.com/"
        };

        IFirebaseClient client;

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
                        .Child(result.FileName)
                        .PutAsync(await result.OpenReadAsync());

                    var downloadlink = await task;
                    downloadUrl = downloadlink;
                }
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
                Shell.Current.DisplayAlert("Error", $"{ex}", "OK");
            }

            return result;
        }

        private async void GetEventNumb()
        {
            ConnectToFirebase();
            FirebaseResponse response = await client.GetAsync(@"Events");

            try
            {
                Events[] result = response.ResultAs<Events[]>();
                numEvents = result.Length;
            }
            catch
            {
                numEvents = 0;
            }
        }
        
        [RelayCommand]
        async void Upload()
        {

            if (Title != null || Details != null || startTime != null || Date != null || State != null || City != null || Address != null)
            {
                location = $"{Address}, {City} {State}";

                GetEventNumb();

                if (downloadUrl == null)
                {
                    downloadUrl = "https://firebasestorage.googleapis.com/v0/b/communityevents-128b1.appspot.com/o/grey.png?alt=media&token=411d50c5-d5f6-4b19-8a76-eb41e301c52f";
                }

                try
                {                 


                    Events events = new Events()
                    {
                        Details = Details,
                        Image = downloadUrl,
                        Name = Title,
                        Location = location,
                        Date = Date,
                        StartTime = StartTime,
                        EndTime = EndTime,
                        EventNumb = numEvents
                    };

                    await client.UpdateAsync($"User/{Userid}/Events/{numEvents}", events);
                    await client.UpdateAsync($"Events/{numEvents}", events);
                }
                finally
                {

                }
            }
            else
            {
                Shell.Current.DisplayAlert("Missing Info", "There is missing information for this event. Please enter data and retry", "OK");
            }
        }
    }
}
