using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using System.Linq;
using SB.Domain.Entities;
using SB.Test.Unit.Gateways;

namespace SB.Test.Unit.Tests
{
    [TestClass]
    public class TaskTest
    {
        private Task Task;

        private TestGateway<Task> Gateway;

        public TaskTest()
        {
            this.Task = new Task("List 1");
            this.Gateway = new TestGateway<Task>();
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
            this.Task.List = new TaskList("List 1");
            this.Task.History = new History("History 1");
            var bs = this.Task.Save(this.Gateway);

            Assert.IsTrue(bs.isSuccess);
            Assert.IsTrue(this.Task.State == State.Modify);
        }

        [TestMethod]
        public void DontSaveTask()
        {
            this.Gateway.SaveDelegated = delegate (Task x) { return false; };
            var bs = this.Task.Save(this.Gateway);

            Assert.IsTrue(this.Task.State == State.Insert);
            Assert.IsFalse(bs.isSuccess);
        }

        [TestMethod]
        public void DontSaveTaskNullList()
        {
            this.Task.History = new History("History 1");
            var bs = this.Task.Save(this.Gateway);

            Assert.IsTrue(this.Task.State == State.Insert);
            Assert.IsFalse(bs.isSuccess);
            Assert.IsTrue(bs.Message.First() == "You must put it in a list.");
        }

        [TestMethod]
        public void DontSaveTaskNullHistory()
        {
            this.Task.List = new TaskList("List 1");
            var bs = this.Task.Save(this.Gateway);

            Assert.IsTrue(this.Task.State == State.Insert);
            Assert.IsFalse(bs.isSuccess);
            Assert.IsTrue(bs.Message.First() == "You must put it in a history.");
        }

        [TestMethod]
        public void FindTask()
        {
            var task = Task.Find(Gateway, 1);

            Assert.IsTrue(task is Task);
        }
    }
}
