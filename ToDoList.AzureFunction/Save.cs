using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ToDoList.Model;
using ToDoList.Repository;

namespace ToDoList.AzureFunction
{
    public static class Save
    {
        [FunctionName("Save")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request in Save.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            ItemToDo data = JsonConvert.DeserializeObject<ItemToDo>(requestBody);
            data.Id = Guid.NewGuid();

            var repository = new ItemRepository();

            await repository.Save(data);

            return new CreatedResult($"", data);
        }
    }
}
