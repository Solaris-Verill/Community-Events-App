using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityEventsMAUI.Models;

namespace CommunityEventsMAUI.Services
{
    public partial class LogOut
    {
        public static async Task logOutAsync ()
        {
            try
            {
                Auth.Role = "";
                Auth.Userid = "";
                Auth.Token = "";

                Admin.adminID = "";

                Shell.Current.Navigation.RemovePage(Shell.Current.CurrentPage);
                await Shell.Current.GoToAsync($"///{nameof(LoginPage)}");

            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex);
            }
        }
    }
}
