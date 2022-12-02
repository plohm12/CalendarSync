using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarSync
{
    public class CalendarSyncService
    {
        private GraphService Source { get; set; }
        private GraphService Destination { get; set; }
        private DataverseService Dataverse { get; }
        private ILogger Log { get; set; }

        public CalendarSyncService(string sourceAccessToken, string destinationAccessToken, ILogger logger)
        {
            Source = new GraphService(sourceAccessToken);
            Destination = new GraphService(destinationAccessToken);
            Dataverse = new DataverseService(logger);
            Log = logger;
        }

        public async Task SyncEventAsync(Event e)
        {
            // get or create event in d365
            var eventCalendarId = await Dataverse.GetOrCreateEventAsync(e.Id, e.Calendar.Id);
            if (eventCalendarId != e.Calendar.Id)
            {
                Log.LogInformation("Event source does not match, skipping sync.");
                return;
            }

            // get event from dest

            // map src event to dest event

            // upsert event in dest
        }
    }
}
