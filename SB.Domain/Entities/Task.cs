using SB.Domain.Interfaces;
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

        public void Save(IDbGateway<Task> gateway)
        {
            try
            {
                this.ValidateTaskInsertion();

                var isSaved = gateway.Save(this);

                this.State = isSaved ? State.Modify : State.Insert;
            }
            finally
            {

            }
        }

        private void ValidateTaskInsertion()
        {
            if (String.IsNullOrEmpty(this.Title))
                throw new Exception(string.Format(Strings.MustSet, "a title"));

            if (this.List == null)
                throw new Exception(string.Format(Strings.MustSet, "a list"));

            if (this.History == null)
                throw new Exception(string.Format(Strings.MustSet, "a history"));

        }

        public static object Find(IDbGateway<Task> gateway, int id)
        {
            return gateway.Find(id);
        }
    }
}