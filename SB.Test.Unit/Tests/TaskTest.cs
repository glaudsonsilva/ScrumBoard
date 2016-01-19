using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using System.Linq;
using SB.Domain.Entities;
using SB.Test.Unit.Repositories;

namespace SB.Test.Unit.Tests
{
    [TestClass]
    public class TaskTest
    {
        private Task Task;

        private TestRepository<Task> Repository;

        public TaskTest()
        {
            this.Task = new Task("List 1");
            this.Repository = new TestRepository<Task>();
        }

        [TestMethod]
        public void AddTaskResponsable()
        {
            this.Task.AddResponsable("user");

            Assert.IsTrue(this.Task.Responsables.Count == 1);
        }

        [TestMethod]
        public void AddTaskTwoResponsable()
        {
            this.Task.AddResponsable("user");
            this.Task.AddResponsable("user");

            Assert.IsTrue(this.Task.Responsables.Count == 2);
        }

        [TestMethod]
        public void AddDescription()
        {
            this.Task.AddDescription("Description of the task");
        }

        [TestMethod]
        public void AddTaskToAList()
        {
            var list = new TaskList("List 1");

            list.AddTask(this.Task);

            Assert.IsTrue(this.Task.List == list);
            Assert.IsTrue(list.Tasks.Count == 1);
        }

        [TestMethod]
        public void AddTaskToHistory()
        {
            var history = new History("History 1");

            history.AddTask(this.Task);

            Assert.IsTrue(history.Tasks.Count == 1);
            Assert.IsTrue(this.Task.History == history);
        }

        [TestMethod]
        public void SaveTask()
        {
            this.CreateAValidTask();
            var notification = this.Task.Save(this.Repository);

            Assert.IsFalse(notification.HasError());
        }

        private void CreateAValidTask()
        {
            this.Task.List = new TaskList("List A");
            this.Task.History = new History("History 1");
        }

        [TestMethod]
        public void DontSaveTask()
        {
            this.Repository.SaveDelegated = delegate (Task x) { return false; };
            this.Task.Save(this.Repository);

            Assert.IsTrue(this.Task.State == State.Insert);
        }

        [TestMethod]
        public void DontSaveTaskNullList()
        {
            this.Task.History = new History("History 1");
            var notification = this.Task.Save(this.Repository);

            Assert.IsTrue(notification.HasError());
            Assert.AreEqual(notification.Errors.First(), "You must put it in a list.");
        }

        [TestMethod]
        public void DontSaveTaskNullHistory()
        {
            this.Task.List = new TaskList("List 1");
            var notification = this.Task.Save(this.Repository);

            Assert.IsTrue(notification.HasError());
            Assert.AreEqual(notification.Errors.First(), "You must put it in a history.");
        }

        [TestMethod]
        public void FindTask()
        {
            var task = Task.Find(Repository, 1);

            Assert.IsTrue(task is Task);
        }

        [TestMethod]
        public void MoveTaskFromListAToListB()
        {
            var listB = new TaskList("List B");

            this.CreateAValidTask();
            this.Task.Save(this.Repository);

            this.Task.Move(listB);

            var notification = this.Task.Save(this.Repository);

            Assert.IsFalse(notification.HasError());
            Assert.AreEqual(this.Task.List, listB);
        }
    }
}
