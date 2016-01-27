using System.Collections.Generic;

namespace SB.Domain.Interfaces
{
    public interface IRepository<T> where T : new()
    {
        void Save(T entity);

        T Find(int id);

        IEnumerable<T> List();
    }
}