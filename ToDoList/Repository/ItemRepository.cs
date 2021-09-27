using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Model;

namespace ToDoList.Repository
{
    public class ItemRepository
    {
        private const string ConnectionString = "AccountEndpoint=https://todolistconta.documents.azure.com:443/;AccountKey=5WkADNH3I8W6ijztIqCPlBSAj4OLhaj5om2upJlXNWPLXlBSHNlNIObhlk8GzVcX6CMG0ye6efQa6nF9QkdSXw==;";
        private const string Database = "todolist-db";
        private const string Container = "list-items";

        private CosmosClient CosmosClient { get; set; }
        public ItemRepository()
        {
            this.CosmosClient = new CosmosClient(ConnectionString);
        }

        public List<ItemToDo> GetAll()
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c");

            var result = new List<ItemToDo>();

            var queryResult = container.GetItemQueryIterator<ItemToDo>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<ItemToDo> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result;
        }

        public ItemToDo GetById(Guid id)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);

            QueryDefinition queryDefinition = new QueryDefinition($"SELECT * FROM c where c.id = '{id}'");

            var result = new List<ItemToDo>();

            var queryResult = container.GetItemQueryIterator<ItemToDo>(queryDefinition);

            while (queryResult.HasMoreResults)
            {
                FeedResponse<ItemToDo> currentResultSet = queryResult.ReadNextAsync().Result;
                result.AddRange(currentResultSet.Resource);
            }

            return result.FirstOrDefault();
        }

        public async Task Save(ItemToDo obj)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.CreateItemAsync<ItemToDo>(obj, new PartitionKey(obj.PartitionKey));
        }

        public async Task Update(ItemToDo obj)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.ReplaceItemAsync<ItemToDo>(obj, obj.Id.ToString(), new PartitionKey(obj.PartitionKey));
        }

        public async Task Delete(ItemToDo obj)
        {
            var container = this.CosmosClient.GetContainer(Database, Container);
            await container.DeleteItemAsync<ItemToDo>(obj.Id.ToString(), new PartitionKey(obj.PartitionKey));
        }
    }
}
