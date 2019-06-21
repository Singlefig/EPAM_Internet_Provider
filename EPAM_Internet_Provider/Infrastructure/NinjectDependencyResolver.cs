using EPAM_Internet_Provider.Domain.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using EPAM_Internet_Provider.DAL.Dao;
using EPAM_Internet_Provider.DAL.Repositories;
using EPAM_Internet_Provider.Services;
using Ninject.Web.Common;

namespace EPAM_Internet_Provider.Infrastructure
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
            kernel.Bind<ProviderContext>().To<ProviderContext>().InSingletonScope();

            kernel.Bind<IUserDao>().To<UserDao>();
            kernel.Bind<IAccountService>().To<AccountService>();

            kernel.Bind<IRateDao>().To<RateDao>();
            kernel.Bind<IRateService>().To<RateService>();
        }
    }
}
