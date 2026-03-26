using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases
{
    public interface IToDoItemRepository
    {

        void Add(TodoItem item);
        TodoItem? GetById(int id);
        void Update(TodoItem item);
        void Delete(int id); void DeleteAll();
        IEnumerable<TodoItem> GetItems();
    }
}
