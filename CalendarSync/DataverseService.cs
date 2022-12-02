using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarSync
{
    public class DataverseService
    {
        private ServiceClient Client { get; set; }
        private ILogger Log { get; set; }

        public DataverseService(ILogger logger)
        {
            Client = new ServiceClient(@"Url=https://org5eed7040.crm.dynamics.com/;AuthType=OAuth;ClientId=51f81489-12ee-4a9e-aaae-a2591f45987d;RedirectUri=http://localhost;");
            Log = logger;
        }

        public async Task<string> GetOrCreateEventAsync(string eventId, string calendarId)
        {
            Log.LogInformation($"Querying Dataverse for event {eventId}");
            var query = new QueryByAttribute("appointment");
            query.AddAttributeValue("pl_graphkey", eventId);
            query.ColumnSet = new ColumnSet("pl_calendarsource");
            query.TopCount = 1;
            var response = await Client.RetrieveMultipleAsync(query);
            if (response.Entities.Any())
            {
                Log.LogInformation("Found a matching appointment.");
                return response.Entities.First().GetAttributeValue<string>("pl_calendarsource");
            }

            query.Attributes.Clear();
            query.Attributes.Add("pl_destinationid");
            response = await Client.RetrieveMultipleAsync(query);

            {
                Log.LogInformation("No match found, creating an appointment.");
                var appt = new Entity("appointment")
                {
                    ["pl_graphkey"] = eventId,
                    ["pl_calendarsource"] = calendarId
                };
                await Client.CreateAsync(appt);
                return calendarId;
            }
        }
    }
}
