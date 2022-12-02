using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CalendarSync
{
    public class GraphService
    {
        private GraphServiceClient Client { get; set; }

        public GraphService(string accessToken)
        {
            var authProvider = new DelegateAuthenticationProvider(request =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                return Task.CompletedTask;
            });
            Client = new GraphServiceClient(authProvider);
        }

        public async Task<Event> GetEventByIdAsync()
        {
            var response = await Client.Me.Calendar.Events.Request().Top(1).GetResponseAsync();
            var eventCollection = await response.GetResponseObjectAsync();
            return eventCollection.Value.Single();
        }
    }
}
