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
    public static class Update
    {
        [FunctionName("Update")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request in Update.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            ItemToDo dataToUpdate = JsonConvert.DeserializeObject<ItemToDo>(requestBody);

            var repository = new ItemRepository();

            await repository.Update(dataToUpdate);

            return new OkObjectResult(dataToUpdate);
        }
    }
}
