using ScrumBoard.Domain.Interfaces;
using ScrumBoard.Domain.Shared;
using System;
using System.Collections.Generic;

namespace ScrumBoard.Domain.Entities
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

        public Notification Save(IRepository<History> repository)
        {
            try
            {
                repository.Save(this);

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