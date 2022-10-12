using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using CommunityEventsMAUI.Models;

namespace CommunityEventsMAUI.Services
{
    public class EventService
    {
        HttpClient httpClient;
        public EventService()
        {
            this.httpClient = new HttpClient();
        }

        List<Events> eventsList;
        public async Task<List<Events>> GetEvents()
        {
            if (eventsList.Count > 0)
            {
                return eventsList;
            }

            var response = await httpClient.GetAsync("https://www.montemagno.com/monkeys.json");
            if (response.IsSuccessStatusCode)
            {
                eventsList = await response.Content.ReadFromJsonAsync<List<Events>>();
            }
            return eventsList;
        }
    }
}
