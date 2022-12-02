using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CalendarSync.Functions
{
    public static class SyncRange
    {
        [FunctionName("SyncRange")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var start = DateTimeOffset.Parse(req.Query["start"]);
            var end = DateTime.Parse(req.Query["end"]);

            var authProvider = new DelegateAuthenticationProvider(request =>
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "");
                return Task.CompletedTask;
            });
            var client = new GraphServiceClient(authProvider);
            client.Me.Calendar.Events.Request();

            return new OkResult();
        }
    }
}
