using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.DataAccess.Model;

namespace ToDoApp.DataAccess.Inerface
{
    /// <author>Malay Dhandha</author>
    /// <created_date>July 09, 2022</created_date>
    /// <description>Repository Interface for To Do</description>
    /// <company>TRIRID</company>
    
    public interface IToDoRepository
    {
        Task<List<ToDo>> GetToDoList();
        Task<ToDo> GetToDoDetailById(int toDoId);
        Task<bool> SaveToDo(ToDo toDoToSave, string mode);
        Task<bool> DeleteToDo(int toDoId);
        Task<bool> UpdateToDoCompletedStatus(int toDoId, bool isCompleted);
    }
}
