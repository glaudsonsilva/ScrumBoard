using SB.Domain.Interfaces;
using SB.Domain.Shared;
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

        public Notification Save(IDbGateway<History> gateway)
        {
            try
            {
                gateway.Save(this);

                this.State = State.Modify;
            }
            catch (Exception ex)
            {
                this.Notification.AddError(ex);
            }
            return this.Notification;
        }
    }
}