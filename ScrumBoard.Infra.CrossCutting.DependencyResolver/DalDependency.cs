using Ninject.Modules;
using ScrumBoard.DAL;
using ScrumBoard.Domain.Entities;
using ScrumBoard.Domain.Interfaces;

namespace ScrumBoard.Infra.CrossCutting.DependencyResolver
{
    public class DalDependency : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IRepository<Board>>().To<BoardInMemoryRepository>();
        }
    }
}
