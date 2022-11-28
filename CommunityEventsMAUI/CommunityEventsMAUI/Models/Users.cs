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
        static public List<Favorites> Favorites;
        static public List<int> Events;
    }
}
