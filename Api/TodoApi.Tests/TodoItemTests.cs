using System;
using System.Collections.Generic;
using TodoApi.Models;
using Xunit;

namespace TodoApi.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void CanCreateTodoItem()
        {
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Sample Task",
                Deadline = "2024-05-20",
                Details = "Sample details",
                IsComplete = false,
                Secret = "Sample secret",
            };

            Assert.NotNull(todoItem);
            Assert.Equal("Sample Task", todoItem.Task);
            Assert.Equal("2024-05-20", todoItem.Deadline);
            Assert.Equal("Sample details", todoItem.Details);
            Assert.False(todoItem.IsComplete);
            Assert.Equal("Sample secret", todoItem.Secret);
        }

        // [Fact]
        // public void CanCreateSubTask()
        // {
        //     var subTask = new SubTask
        //     {
        //         Id = Guid.NewGuid(),
        //         Task = "Sub Task",
        //         Deadline = "2024-05-18",
        //         Details = "Details for sub task",
        //         IsComplete = false,
        //         Secret = "Sub task secret"
        //     };

        //     Assert.NotNull(subTask);
        //     Assert.Equal("Sub Task", subTask.Task);
        //     Assert.Equal("2024-05-18", subTask.Deadline);
        //     Assert.Equal("Details for sub task", subTask.Details);
        //     Assert.False(subTask.IsComplete);
        //     Assert.Equal("Sub task secret", subTask.Secret);
        // }
    }
}
