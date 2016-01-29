using System;
using System.Collections.Generic;
using ScrumBoard.Domain.Interfaces;

namespace ScrumBoard.Test.Unit.Repositories
{
    public class TestRepository<T> : IRepository<T> where T : new()
    {
        public delegate bool SaveDelegate(T task);

        public SaveDelegate SaveDelegated;

        public TestRepository() { }

        public TestRepository(SaveDelegate saveDelegated)
        {
            this.SaveDelegated = saveDelegated;
        }

        public void Save(T task)
        {
            if (this.SaveDelegated != null)
                this.SaveDelegated.Invoke(task);
        }

        public T Find(int id)
        {
            return new T();
        }

        public IEnumerable<T> List()
        {
            throw new NotImplementedException();
        }
    }
}
