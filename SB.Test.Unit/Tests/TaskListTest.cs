using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using System.Linq;
using SB.Domain.Entities;
using SB.Test.Unit.Gateways;

namespace SB.Test.Unit.Tests
{
    [TestClass]
    public class TaskListTest
    {
        private TaskList TaskList;

        public TaskListTest()
        {
            this.TaskList = new TaskList("List 1");
        }

        [TestMethod]
        public void AddTask()
        {
            var task = new Task("Clean the house");
            this.TaskList.AddTask(task);

            Assert.IsTrue(this.TaskList.Tasks.Count == 1);
            Assert.IsTrue(task.List == this.TaskList);
        }

        [TestMethod]
        public void RemoveTaskAfterAdd()
        {
            this.TaskList.AddTask("Clean the house");
            this.TaskList.RemoveTask("Clean the house");

            Assert.IsTrue(this.TaskList.Tasks.Count == 0);
        }

        [TestMethod]
        public void GetTaskPosition()
        {
            this.AddSeveralTasks();

            Assert.IsTrue(this.TaskList.FindTask("Task 2").Position == 1);
        }

        [TestMethod]
        public void GetTaskPositionAfterRemoveOneInTheMiddle()
        {
            this.AddSeveralTasks();

            Assert.IsTrue(this.TaskList.FindTask("Task 2").Position == 1);

            this.TaskList.RemoveTask("Task 2");

            Assert.IsTrue(this.TaskList.FindTask("Task 3").Position == 1);
        }

        [TestMethod]
        public void ChangeTaskPositionFirstToLast()
        {
            this.AddSeveralTasks();

            var firstTask = this.TaskList.Tasks.First();

            var lastTask = this.TaskList.Tasks.Last();

            var lastTaskPosition = lastTask.Position;

            this.TaskList.ChangeTaskPosition(firstTask, lastTaskPosition);

            Assert.IsTrue(firstTask.Position == lastTaskPosition);
            Assert.IsTrue(lastTask.Position == lastTaskPosition - 1);
        }

        [TestMethod]
        public void ChangeTaskPositionFourthToSixth()
        {
            this.AddSeveralTasks();

            var fourthTask = this.TaskList.Tasks[3];

            var sixthTask = this.TaskList.Tasks[5];
            var sixthPosition = sixthTask.Position;

            this.TaskList.ChangeTaskPosition(fourthTask, sixthPosition);

            Assert.IsTrue(fourthTask.Position == sixthPosition);
            Assert.IsTrue(sixthTask.Position == sixthPosition - 1);
        }

        [TestMethod]
        public void ChangeTaskPositionSixthToSecond()
        {
            this.AddSeveralTasks();

            var sixthTask = this.TaskList.Tasks[5];
            var secondTask = this.TaskList.Tasks[1];

            var secondPosition = secondTask.Position;

            this.TaskList.ChangeTaskPosition(sixthTask, secondPosition);

            Assert.IsTrue(sixthTask.Position == secondPosition);
            Assert.IsTrue(secondTask.Position == secondPosition + 1);
        }

        [TestMethod]
        public void SaveTaskList()
        {
            var notification = this.TaskList.Save(new TestGateway<TaskList>());

            Assert.IsFalse(notification.HasError());
        }

        private void AddSeveralTasks()
        {
            this.TaskList.AddTask("Task 1");
            this.TaskList.AddTask("Task 2");
            this.TaskList.AddTask("Task 3");
            this.TaskList.AddTask("Task 4");
            this.TaskList.AddTask("Task 5");
            this.TaskList.AddTask("Task 6");
            this.TaskList.AddTask("Task 7");
            this.TaskList.AddTask("Task 8");
            this.TaskList.AddTask("Task 9");
        }
    }
}
