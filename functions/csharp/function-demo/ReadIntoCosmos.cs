using System;
using System.Threading.Tasks;
using function_demo.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace function_demo
{
    public static class ReadIntoCosmos
    {
        [FunctionName("ReadIntoCosmos")]
        public static async Task Run(
            [QueueTrigger("msg-001-queue", Connection = "AZURE_STORAGE_QUEUE")]string queueItem,
            [CosmosDB(
                databaseName: "mjr-063-cosmos-db",
                collectionName: "orders",
                ConnectionStringSetting = "AZURE_COSMOS_KEY")]
                IAsyncCollector<EndMessage> docs,
            ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {queueItem}");

            var item = JsonConvert.DeserializeObject<EndMessage>(queueItem);
            await docs.AddAsync(item).ConfigureAwait(false);
        }
    }
}
