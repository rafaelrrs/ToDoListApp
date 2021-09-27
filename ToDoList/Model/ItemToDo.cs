using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Model
{
    public class ItemToDo
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "criadoPor")]
        public String CriadoPor { get; set; }

        [JsonProperty(PropertyName = "status")]
        public Boolean Status { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }

        [JsonProperty(PropertyName = "descricao")]
        public string Descricao { get; set; }

        [JsonProperty(PropertyName = "pk")]
        public string PartitionKey { get; set; } = "todolist";
    }
}
