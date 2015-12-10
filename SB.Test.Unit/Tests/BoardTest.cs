using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Domain;
using SB.Domain.Entities;
using SB.Test.Unit.Gateways;

namespace SB.Test.Unit.Tests
{
    [TestClass]
    public class BoardTest
    {
        private Board Board;
        private const string LIST_1_NAME = "List 1";
        private TestGateway<Board> Gateway;

        public BoardTest()
        {
            this.Board = new Board();
            this.Gateway = new TestGateway<Board>();
        }

        [TestMethod]
        public void AddListOfTasks()
        {
            this.Board.AddList(LIST_1_NAME);

            Assert.IsTrue(this.Board.TaskList.Count == 1);
        }

        [TestMethod]
        public void FindListOfTasks()
        {
            this.Board.AddList(LIST_1_NAME);

            var list = this.Board.FindList(LIST_1_NAME);

            Assert.IsTrue(list != null);
            Assert.IsTrue(list.Name == LIST_1_NAME);
        }

        [TestMethod]
        public void RemoveListOfTasks()
        {
            this.Board.AddList(LIST_1_NAME);

            var list = this.Board.FindList(LIST_1_NAME);

            this.Board.RemoveList(list);

            Assert.IsTrue(this.Board.TaskList.Count == 0);
        }

        [TestMethod]
        public void ChangeSecondListPositionToFourth()
        {
            AddSeveralLists();

            var secondList = this.Board.FindList("2");
            var fourthList = this.Board.FindList("4");

            var fourthListPosition = fourthList.Position;

            this.Board.ChangeTaskListPosition(secondList, fourthList.Position);

            Assert.IsTrue(secondList.Position == fourthListPosition);
            Assert.IsTrue(fourthList.Position == fourthListPosition - 1);
        }

        [TestMethod]
        public void ChangeFirstListPositionToSecond()
        {
            AddSeveralLists();

            var firstList = this.Board.FindList("1");
            var secondList = this.Board.FindList("2");

            var fourthListPosition = secondList.Position;

            this.Board.ChangeTaskListPosition(firstList, secondList.Position);

            Assert.IsTrue(firstList.Position == fourthListPosition);
            Assert.IsTrue(secondList.Position == fourthListPosition - 1);
        }

        [TestMethod]
        public void ChangeFifthListPositionToThird()
        {
            AddSeveralLists();

            var fifthList = this.Board.FindList("5");
            var thirdList = this.Board.FindList("3");

            var fourthListPosition = thirdList.Position;

            this.Board.ChangeTaskListPosition(fifthList, thirdList.Position);

            Assert.IsTrue(fifthList.Position == fourthListPosition);
            Assert.IsTrue(thirdList.Position == fourthListPosition + 1);
        }

        [TestMethod]
        public void SaveBoard()
        {
            this.AddSeveralLists();
            this.Board.Save(this.Gateway, new TestGateway<TaskList>(), new TestGateway<History>());

            Assert.IsTrue(this.Board.State == State.Modify);

            foreach (var taskList in this.Board.TaskList)
                Assert.IsTrue(taskList.State == State.Modify);

            foreach (var history in this.Board.Histories)
                Assert.IsTrue(history.State == State.Modify);
        }

        [TestMethod]
        public void DontSaveBoard()
        {
            this.Gateway.SaveDelegated = delegate (Board x) { return false; };
            this.Board.Save(this.Gateway, new TestGateway<TaskList>(), new TestGateway<History>());

            Assert.IsTrue(this.Board.State == State.Insert);
        }

        private void AddSeveralLists()
        {
            this.Board.AddList("1");
            this.Board.AddList("2");
            this.Board.AddList("3");
            this.Board.AddList("4");
            this.Board.AddList("5");
        }
    }
}
