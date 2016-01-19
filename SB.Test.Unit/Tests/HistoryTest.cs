﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using SB.Domain.Entities;
using SB.Test.Unit.Repositories;

namespace SB.Test.Unit.Tests
{
    [TestClass]
    public class HistoryTest
    {
        public History History { get; set; }

        public HistoryTest()
        {
            this.History = new History("History 1");
        }

        [TestMethod]
        public void AddDescription()
        {
            this.History.AddDescription("Description history 1");
        }

        [TestMethod]
        public void AddTask()
        {
            var task = new Task("Task 1");
            this.History.AddTask(task);

            Assert.IsTrue(this.History.Tasks.Count == 1);
            Assert.IsTrue(task.History == this.History);
        }

        [TestMethod]
        public void SaveHistory()
        {
            var notification = this.History.Save(new TestRepository<History>());

            Assert.IsFalse(notification.HasError());
        }

    }
}
