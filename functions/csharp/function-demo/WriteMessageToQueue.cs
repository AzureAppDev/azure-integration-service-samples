using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using function_demo.Models;

namespace function_demo
{
    [StorageAccount("AZURE_STORAGE_QUEUE")]
    public static class WriteMessageToQueue
    {
        [FunctionName("WriteMessageToQueue")]
        [return: Queue("msg-001-queue")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            // Deserialize the body of the request
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            InputMessage message = JsonConvert.DeserializeObject<InputMessage>(requestBody);

            // Convert to the correct message
            var result = new EndMessage()
            {
                id = message.id,
                source = message.source,
                date = message.purchasedOn,
                customerId = message.userId,
                description = message.description,
                amount = message.amount
            };

            // Return the result as a serialized string
            return JsonConvert.SerializeObject(result);
        }
    }
}
