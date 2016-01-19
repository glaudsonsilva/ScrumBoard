using SB.Domain.Interfaces;
using SB.Domain.Shared;
using System;
using System.Collections.Generic;

namespace SB.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; private set; }

        public int Position { get; set; }

        public List<String> Responsables { get; private set; }

        public string Description { get; private set; }

        public TaskList List { get; set; }

        public History History { get; set; }

        public Task() : this("") { }

        public Task(string title)
        {
            this.Title = title;
            this.Responsables = new List<string>();
            this.State = State.Insert;
        }

        public void AddResponsable(string user)
        {
            this.Responsables.Add(user);
        }

        public void AddDescription(string description)
        {
            this.Description = description;
        }

        public Notification Save(IRepository<Task> repository)
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
            if (String.IsNullOrEmpty(this.Title))
                this.Notification.AddError(string.Format(Strings.MustPutIn, "a title"));

            if (this.List == null)
                this.Notification.AddError(string.Format(Strings.MustPutIn, "a list"));

            if (this.History == null)
                this.Notification.AddError(string.Format(Strings.MustPutIn, "a history"));
        }

        public static object Find(IRepository<Task> repository, int id)
        {
            return repository.Find(id);
        }

        public void Move(TaskList listB)
        {
            this.List = listB;
        }
    }
}