using System.Net.Http.Json;

namespace CommunityEventsMAUI.Services
{
    public class EventService
    {
        HttpClient httpClient;
        public EventService()
        {
            httpClient = new HttpClient();
        }

        List<Events> eventList = new List<Events>();

        public async Task<List<Events>> GetEvents()
        {
            if(eventList?.Count > 0)
            {
                return eventList;
            }

            var url = "https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json";
            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                eventList = await response.Content.ReadFromJsonAsync<List<Events>>();
            }

            return null;
        }
    }
}
