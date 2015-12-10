using System.Linq;
using System.Collections.Generic;
using System;
using SB.Domain.Interfaces;

namespace SB.Domain.Entities
{
    public class Board : BaseEntity
    {
        public IList<TaskList> TaskList { get; private set; }

        public IList<History> Histories { get; private set; }

        public Board()
        {
            this.TaskList = new List<TaskList>();
            this.Histories = new List<History>();
            this.State = State.Insert;
        }

        public void AddList(string name)
        {
            var list = new TaskList(name);
            list.Position = this.TaskList.Count;

            this.TaskList.Add(list);
        }

        public TaskList FindList(string name)
        {
            return this.TaskList.Where(x => x.Name == name).FirstOrDefault();
        }

        public void RemoveList(TaskList list)
        {
            this.TaskList.Remove(list);

            this.ReorderTaskList();
        }

        public void ChangeTaskListPosition(TaskList list, int position)
        {
            this.TaskList.Remove(list);

            this.TaskList.Insert(position, list);

            this.ReorderTaskList();
        }

        private void ReorderTaskList()
        {
            for (int i = 0; i < this.TaskList.Count; i++)
            {
                this.TaskList[i].Position = i;
            }
        }

        public bool Save(IDbGateway<Board> gateway, IDbGateway<TaskList> taskListGateway, IDbGateway<History> historyGateway)
        {
            var isSaved = gateway.Save(this);

            if (isSaved)
            {
                foreach (var taskList in this.TaskList)
                    taskList.Save(taskListGateway);

                foreach (var history in this.Histories)
                    history.Save(historyGateway);

                this.State = isSaved ? State.Modify : State.Insert;
            }

            return isSaved;
        }
    }
}
