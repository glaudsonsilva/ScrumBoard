using System;
using System.Collections.Generic;
using System.Linq;

namespace SB.Domain.Shared
{
    public class Notification
    {
        public List<String> Errors { get; set; }

        public Notification()
        {
            this.Errors = new List<string>();
        }

        public void AddError(string error)
        {
            this.Errors.Add(error);
        }

        public bool HasError()
        {
            return this.Errors.Any();
        }

        internal void AddError(Exception ex)
        {
            this.AddError(ex.Message);
        }
    }
}
