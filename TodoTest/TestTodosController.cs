using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoService.Logics;
using TodoService.Models;

namespace TodoTest
{
    [TestClass]
    public class TestTodosController
    {
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

            var service = new TodoLogic(mockContext.Object);
            Assert.AreEqual(new Guid("7dddfd22-f70d-41ae-9040-293c81acb049"), service.GetTodoById(data.FirstOrDefault().Id).Id);
        }
    }
}
