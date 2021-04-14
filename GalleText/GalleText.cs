using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace GalleText
{
    public static class GalleText
    {
        [FunctionName("Galle")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var rawText = req.Form["text"].FirstOrDefault();

            log.LogInformation($"Gallifying text {rawText}");

            if (rawText == null)
                return new OkObjectResult(null);

            var enumerable = rawText.Select(x => x == ' ' ? "      " : $":alphabet-white-{x}:");
            var newstring = string.Join(null, enumerable);

            return new JsonResult(new {
                response_type = "in_channel",
                text = newstring
            });
        }
    }
}

