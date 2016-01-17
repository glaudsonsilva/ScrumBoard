
using SB.Domain.Shared;

namespace SB.Domain.Interfaces
{
    public interface IDbGateway<T> where T : new()
    {
        void Save(T entity);

        T Find(int id);
    }
}