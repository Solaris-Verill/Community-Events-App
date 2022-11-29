using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityEventsMAUI.Models
{
    public class Users
    {
        public string Username { get; set; }
        public string Role { get; set; }
        static public List<Events> Favorites;
        static public List<Events> Events;
    }
}
