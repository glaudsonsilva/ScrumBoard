using System.Linq;
using System.Collections.Generic;
using System;
using SB.Domain.Interfaces;
using SB.Domain.Shared;

namespace SB.Domain.Entities
{
    public class Board : BaseEntity
    {
        public IList<TaskList> TaskList { get; private set; }

        public IList<History> Histories { get; private set; }

        public string Name { get; set; }

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

        public Notification Save(IRepository<Board> repository)
        {
            try
            {
                this.ValidateTaskInsertion();

                if (this.Notification.HasError())
                    return this.Notification;

                repository.Save(this);

                this.State = State.Modify;
            }
            catch (Exception ex)
            {
                this.Notification.AddError(ex);
            }
            return this.Notification;
        }

        private void ValidateTaskInsertion()
        {
            if (String.IsNullOrWhiteSpace(this.Name))
                this.Notification.AddError(string.Format(Strings.MustSet, "a name"));
        }
    }
}
