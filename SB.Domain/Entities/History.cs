using SB.Domain.Interfaces;
using System;
using System.Collections.Generic;

namespace SB.Domain.Entities
{
    public class History : BaseEntity
    {
        private string Title;

        public List<Task> Tasks { get; private set; }
        public string Description { get; private set; }

        public History() : this("") { }

        public History(string title)
        {
            this.Tasks = new List<Task>();

            this.Title = title;
        }

        public void AddDescription(string description)
        {
            this.Description = description;
        }

        public void AddTask(Task task)
        {
            this.Tasks.Add(task);

            task.History = this;
        }

        public BService Save(IDbGateway<History> gateway)
        {
            var bs = new BService { isSuccess = true };

            bs.isSuccess = gateway.Save(this);

            this.State = bs.isSuccess ? State.Modify : State.Insert;

            return bs;
        }
    }
}