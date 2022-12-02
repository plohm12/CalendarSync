using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;

namespace CalendarSync.Functions
{
    public static class Subscribe
    {
        [FunctionName("Subscribe")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string validationToken = req.Query["validationToken"];
            if (!string.IsNullOrEmpty(validationToken))
            {
                return new OkObjectResult(validationToken);
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            log.LogInformation(requestBody);
            //dynamic data = JsonConvert.DeserializeObject(requestBody);


            return new OkResult();
        }
    }
}
