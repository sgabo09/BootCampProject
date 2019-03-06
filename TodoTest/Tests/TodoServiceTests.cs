using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http.OData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoService.Logics;
using TodoService.Models;

namespace TodoTest
{
    [TestClass]
    public class TestTodoService
    {
        [TestMethod]
        public void PostTodo()
        {
            // Arrange
            var data = new List<Todo>
            {
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"),
                    Name = "Landing page",
                    Description = "Home page with header menu",
                    Priority = 1,
                    Responsible = "Johnny",
                    Status = "To Do",
                    Deadline = new DateTime(2019 - 03 - 12)
                }
            };

            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<TodoContext>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);

            var service = new TodoLogic(mockContext.Object);

            var mockTodo = new Todo
            {
                Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb050"),
                Name = "Mocked todo",
                Description = "It is moqed",
                Priority = 3,
                Responsible = "Mo",
                Status = "Open",
                Deadline = new DateTime(2019,03,14)
            };

            // Act
            service.PostTodo(mockTodo);

            // Assert
            Assert.AreNotEqual(mockTodo.Id, new Guid("7dddfd22-f70d-41ae-9040-293c81acb050"));
            Assert.IsInstanceOfType(mockTodo.CreationDate, typeof(DateTime));
            mockSet.Verify(m => m.Add(It.IsAny<Todo>()), Times.AtLeast(1));
            mockContext.Verify(m => m.SaveChanges(), Times.AtLeast(1));
            
        }

        [TestMethod]
        public void GetTodosById()
        {
            var data = new List<Todo>

            {
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"),
                    Name = "Landing page",
                    Description = "Home page with header menu",
                    Priority = 1,
                    Responsible = "Johnny",
                    Status = "To Do",
                    Deadline = new DateTime(2019,3,15)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<TodoContext>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);

            var service = new TodoLogic(mockContext.Object);

            Assert.AreEqual("Landing page", service.GetTodoById(new Guid("7dddfd22-f70d-41ae-9040-293c81acb049")).Name);
        }

        [TestMethod]
        public void UpdateTodo()
        {
            var data = new List<Todo>
            {
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"),
                    Name = "Landing page",
                    Description = "Home page with header menu",
                    Priority = 1,
                    Responsible = "Johnny",
                    Status = "To Do",
                    Deadline = new DateTime(2019 - 03 - 12)
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<TodoContext>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
            mockContext.Setup(m => m.Todos.Find(It.IsAny<object[]>())).Returns<object[]>(i => data.FirstOrDefault(d => d.Id == (Guid)i[0]));

            var service = new TodoLogic(mockContext.Object);

            var delta = new Delta<Todo>(typeof(Todo));
            delta.TrySetPropertyValue("Responsible", "Sanyi");


            var result = service.PatchTodo(new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"), delta);


            Assert.IsTrue(result);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void DeleteTodo()
        {
            var data = new List<Todo>
            {
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"),
                    Name = "Landing page",
                    Description = "Home page with header menu",
                    Priority = 1,
                    Responsible = "Johnny",
                    Status = "To Do",
                    Deadline = new DateTime(2019 - 03 - 12)
                }
            };

            var queryableData = data.AsQueryable();
            var mockSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<TodoContext>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
            mockContext.Setup(m => m.Todos.Find(It.IsAny<object[]>())).Returns<object[]>(i => data.FirstOrDefault(d => d.Id == (Guid)i[0]));

            var service = new TodoLogic(mockContext.Object);

            var result = service.DeleteTodo(new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"));


            Assert.AreEqual(true, data.FirstOrDefault()?.IsDeleted);
            Assert.IsTrue(result);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void CheckRecentTodos()
        {
            var data = new List<Todo>
            {
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"),
                    Name = "Landing page",
                    Description = "Home page with header menu",
                    Priority = 1,
                    Responsible = "Johnny",
                    Status = "To-Do",
                    Deadline = new DateTime(2019, 03, 12),
                    LastModified = DateTime.Now
                },
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb050"),
                    Name = "Home",
                    Description = "CSS",
                    Priority = 1,
                    Responsible = "Joseph",
                    Status = "Open",
                    Deadline = new DateTime(2019, 03, 12),
                    LastModified = DateTime.Now.AddHours(-3)
                },
                new Todo
                {
                    Id = new Guid("7dddfd22-f70d-41ae-9040-293c81acb051"),
                    Name = "User",
                    Description = "UI",
                    Priority = 1,
                    Responsible = "Charles",
                    Status = "To-Do",
                    Deadline = new DateTime(2019, 03, 12),
                    LastModified = DateTime.Now.AddHours(-5)
                }
            }.AsEnumerable();

            var queryableData = data.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            var mockContext = new Mock<TodoContext>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);

            var service = new TodoLogic(mockContext.Object);


            var result = service.GetRecentTodos();


            Assert.AreEqual(2, result.Count());
        }
    }
}