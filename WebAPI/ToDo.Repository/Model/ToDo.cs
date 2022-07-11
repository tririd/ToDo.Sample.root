using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.DataAccess.Model
{

    public class ToDo 
    {
        public int ToDoId { get; set; }

        public string Title { get; set; }
        public bool IsCompleted { get; set; } 

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
