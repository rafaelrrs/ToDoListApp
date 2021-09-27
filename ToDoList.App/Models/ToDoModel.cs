using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.App.Models
{
    public class ToDoModel
    {
        public Guid Id { get; set; }
        [Required]
        public String CriadoPor { get; set; }

        public Boolean Status { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Descricao { get; set; }
    }
}
