using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;

namespace GameStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { Name = "SimCity", Price = 1 },
                new Game { Name = "Heroes", Price=100000500000 },
                new Game { Name = "Morrowind", Price=100000050000 },
                new Game { Name = "TITANFALL", Price=1 },
                new Game { Name = "Lol", Price=77 },
                new Game { Name = "olololo", Price=2299 },
                new Game { Name = "Battlefield V", Price=1 }
            });
            kernel.Bind<IGameRepository>().ToConstant(mock.Object);
        }
    }
}