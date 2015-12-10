
namespace SB.Domain.Interfaces
{
    public interface IDbGateway<T> where T : new()
    {
        bool Save(T entity);

        T Find(int id);
    }
}