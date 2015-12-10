using System;
using System.Collections.Generic;

namespace SB.Domain
{
    public class BService
    {
        public BService()
        {
            this.Message = new List<string>();
        }
        public bool isSuccess { get; set; }

        public List<string> Message { get; private set; }

        internal void AddValidationMessage(string message)
        {
            this.isSuccess = false;
            this.Message.Add(message);
        }
        internal void AddValidationMessage(string message, string value)
        {
            this.AddValidationMessage(string.Format(message, value));
        }
    }
}
