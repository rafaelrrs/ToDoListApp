using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.App.Models;

namespace ToDoList.App.Infra
{
    public class ToDoRestClient
    {
        private string URL_TODO_REST = "https://todolist74100.azurewebsites.net/api/";

        public IList<ToDoModel> GetAll()
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest("GetAll", DataFormat.Json);
            var response = client.Get<IList<ToDoModel>>(request);

            return response.Data;
        }

        public ToDoModel GetById(Guid id)
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest($"GetById?id={id}", DataFormat.Json);
            var response = client.Get<ToDoModel>(request);

            return response.Data;
        }

        public void Save(ToDoModel model)
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest($"Save", DataFormat.Json);
            request.AddJsonBody(model);
            var response = client.Post<ToDoModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception("Erro na criação de ToDo");
        }

        public void Delete(Guid id)
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest($"Delete?id={id}", DataFormat.Json);
            var response = client.Delete<ToDoModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Erro ao excluir ToDo");
        }

        public void Update(ToDoModel model)
        {
            var client = new RestClient(URL_TODO_REST);
            var request = new RestRequest($"Update", DataFormat.Json);
            request.AddJsonBody(model);
            var response = client.Put<ToDoModel>(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Erro na alteração de ToDo");
        }
    }
}
