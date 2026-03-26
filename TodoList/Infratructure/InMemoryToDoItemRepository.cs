using Entities;
using UseCases;

namespace Infratructure
{
    public class InMemoryToDoItemRepository : IToDoItemRepository
    {
        private readonly List<TodoItem> items = new();

        public IEnumerable<TodoItem> GetItems()
        {
            return items;
        }

        public TodoItem? GetById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public void Add(TodoItem item)
        {
            items.Add(item);
        }

        public void Update(TodoItem item)
        {
            var index = items.FindIndex(i => i.Id == item.Id);
            if (index == -1)
                throw new Exception("Item not found");

            items[index] = item;
        }

        public void Delete(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        public void DeleteAll()
        {
            items.Clear();
        }
    }
}