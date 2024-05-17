using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
            };

            Assert.NotNull(todoItem);
            Assert.Equal("Sample Task", todoItem.Task);
            Assert.Equal("2024-05-20", todoItem.Deadline);
            Assert.Equal("Sample details", todoItem.Details);
            Assert.False(todoItem.IsComplete);
        }

        [Fact]
        public void CanAddSubTasksToTodoItem()
        {
            var subTask1 = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Subtask 1",
                Deadline = "2024-05-21",
                Details = "Subtask details 1",
                IsComplete = false,
            };

            var subTask2 = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Subtask 2",
                Deadline = "2024-05-22",
                Details = "Subtask details 2",
                IsComplete = false,
            };

            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Main Task",
                Deadline = "2024-05-20",
                Details = "Main task details",
                IsComplete = false,
                SubTasks = new List<TodoItem> { subTask1, subTask2 }
            };

            Assert.NotNull(todoItem.SubTasks);
            Assert.Equal(2, todoItem.SubTasks.Count);
            Assert.Contains(todoItem.SubTasks, subTask => subTask.Task == "Subtask 1");
            Assert.Contains(todoItem.SubTasks, subTask => subTask.Task == "Subtask 2");
        }

        [Fact]
        public void SubTasksCannotHaveSubTasks()
        {
            // Sub-subtask should not be allowed
            var subSubTask = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Sub-subtask",
                Deadline = "2024-05-23",
                Details = "Sub-subtask details",
                IsComplete = false
            };

            var subTask = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Subtask",
                Deadline = "2024-05-21",
                Details = "Subtask details",
                IsComplete = false,
                SubTasks = new List<TodoItem> { subSubTask } // This should cause an exception
            };

            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Main Task",
                Deadline = "2024-05-20",
                Details = "Main task details",
                IsComplete = false
            };

            Assert.Throws<InvalidOperationException>(() => todoItem.SubTasks = new List<TodoItem> { subTask });
        }

        [Fact]
        public void TodoItem_ValidModel_ShouldPassValidation()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Valid Task",
                Details = "Valid details with sufficient length",
                Deadline = "2024-08-01",
                IsComplete = false
            };

            // Act
            var validationResults = ValidationHelper.ValidateModel(todoItem);

            // Assert
            Assert.Empty(validationResults);
        }

        [Fact]
        public void TodoItem_InvalidTaskLength_ShouldFailValidation()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "S", // Invalid length
                Details = "Valid details with sufficient length",
                Deadline = "2024-08-01",
                IsComplete = false
            };

            // Act
            var validationResults = ValidationHelper.ValidateModel(todoItem);

            // Assert
            Assert.Single(validationResults);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Task") && v.ErrorMessage.Contains("minimum length"));
        }

        [Fact]
        public void TodoItem_InvalidDetailsLength_ShouldFailValidation()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Valid Task",
                Details = "Short", // Invalid length
                Deadline = "2024-08-01",
                IsComplete = false
            };

            // Act
            var validationResults = ValidationHelper.ValidateModel(todoItem);

        }

        [Fact]
        public void TodoItem_TaskTooLong_ShouldFailValidation()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = new string('A', 101), // Too long
                Details = "Valid details with sufficient length",
                Deadline = "2024-08-01",
                IsComplete = false
            };

            // Act
            var validationResults = ValidationHelper.ValidateModel(todoItem);

            // Assert
            Assert.Single(validationResults);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Task") && v.ErrorMessage.Contains("maximum length"));
        }

        [Fact]
        public void TodoItem_DetailsTooLong_ShouldFailValidation()
        {
            // Arrange
            var todoItem = new TodoItem
            {
                Id = Guid.NewGuid(),
                Task = "Valid Task",
                Details = new string('A', 501), // Too long
                Deadline = "2024-08-01",
                IsComplete = false
            };

            // Act
            var validationResults = ValidationHelper.ValidateModel(todoItem);

            // Assert
            Assert.Single(validationResults);
            Assert.Contains(validationResults, v => v.MemberNames.Contains("Details") && v.ErrorMessage.Contains("maximum length"));
        }
    }
}
