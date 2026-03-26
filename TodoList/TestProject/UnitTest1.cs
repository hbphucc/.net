using Entities;
using UseCases;
using Infratructure;

namespace TestProject
{
    public class UnitTest1
    {
        [Fact]
        public void CreateTodoItem_And_Set_Completed_Test()
        {
            // Arrange
            //var items = new List<TodoItem>
            //{
            //    new TodoItem { Id = 1, Title = "Test1", IsCompleted = false },
            //    new TodoItem { Id = 2, Title = "Test2", IsCompleted = true }
            //};
            var mockRerpository = new InMemoryToDoItemRepository();
            var todoListManager = new TodoListManager(mockRerpository);

            var todoItem = new TodoItem { Id = 1, Text = "Test1", IsCompleted = false };

            // Act
            todoListManager.AddTodoItem(todoItem);

            todoListManager.MarkAsCompleted(1);
            // Assert
            Assert.True(todoListManager.getTodoItems().First().IsCompleted);
            Assert.Equal("Test1", todoListManager.getTodoItems().First().Text);
        }

    //    [Fact]
    //    public void AddTodoItem_CallsRepositoryAdd()
    //    {
    //        // Arrange
    //        var item = new TodoItem { Id = 1, Title = "Test", IsCompleted = false };
    //        var repoMock = new Mock<IToDoItemRepository>();
    //        var manager = new TodoListManager(repoMock.Object);

    //        // Act
    //        manager.AddTodoItem(item);

    //        // Assert
    //        repoMock.Verify(r => r.Add(item), Times.Once);
    //    }

    //    [Fact]
    //    public void MarkAsCompleted_ItemExists_UpdatesItem()
    //    {
    //        // Arrange
    //        var item = new TodoItem { Id = 1, Title = "Test", IsCompleted = false };
    //        var repoMock = new Mock<IToDoItemRepository>();
    //        repoMock.Setup(r => r.GetById(1)).Returns(item);
    //        var manager = new TodoListManager(repoMock.Object);

    //        // Act
    //        manager.MarkAsCompleted(1);

    //        // Assert
    //        Assert.True(item.IsCompleted);
    //        repoMock.Verify(r => r.Update(item), Times.Once);
    //    }

    //    [Fact]
    //    public void MarkAsCompleted_ItemDoesNotExist_DoesNothing()
    //    {
    //        // Arrange
    //        var repoMock = new Mock<IToDoItemRepository>();
    //        repoMock.Setup(r => r.GetById(1)).Returns((TodoItem?)null);
    //        var manager = new TodoListManager(repoMock.Object);

    //        // Act
    //        manager.MarkAsCompleted(1);

    //        // Assert
    //        repoMock.Verify(r => r.Update(It.IsAny<TodoItem>()), Times.Never);
    //    }

    //    [Fact]
    //    public void Delete_CallsRepositoryDelete()
    //    {
    //        // Arrange
    //        var repoMock = new Mock<IToDoItemRepository>();
    //        var manager = new TodoListManager(repoMock.Object);

    //        // Act
    //        manager.Delete(1);

    //        // Assert
    //        repoMock.Verify(r => r.Delete(1), Times.Once);
    //    }
    }
}