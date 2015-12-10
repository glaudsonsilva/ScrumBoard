using SB.Domain.Interfaces;

namespace SB.Test.Unit.Gateways
{
    public class TestGateway<T> : IDbGateway<T> where T : new()
    {
        public delegate bool SaveDelegate(T task);

        public SaveDelegate SaveDelegated;

        public TestGateway() { }

        public TestGateway(SaveDelegate saveDelegated)
        {
            this.SaveDelegated = saveDelegated;
        }

        public bool Save(T task)
        {
            if (this.SaveDelegated != null)
                return this.SaveDelegated.Invoke(task);

            return task != null;
        }

        public T Find(int id)
        {
            return new T();
        }
    }
}
