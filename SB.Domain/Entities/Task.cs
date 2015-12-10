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

        public BService Save(IDbGateway<Task> gateway)
        {
            var bs = this.ValidateTaskInsertion();

            if (!bs.isSuccess)
                return bs;

            var isSaved = gateway.Save(this);

            this.State = isSaved ? State.Modify : State.Insert;

            bs.isSuccess = true;

            return bs;
        }

        private BService ValidateTaskInsertion()
        {
            var bs = new BService { isSuccess = true };

            if (String.IsNullOrEmpty(this.Title))
                bs.AddValidationMessage(Strings.MustSet, "a title");

            if (this.List == null)
                bs.AddValidationMessage(Strings.MustPutIn, "a list");

            if (this.History == null)
                bs.AddValidationMessage(Strings.MustPutIn, "a history");

            return bs;
        }

        public static object Find(IDbGateway<Task> gateway, int id)
        {
            return gateway.Find(id);
        }
    }
}