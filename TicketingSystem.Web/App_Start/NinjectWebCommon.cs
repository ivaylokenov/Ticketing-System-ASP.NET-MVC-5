[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TicketingSystem.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TicketingSystem.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TicketingSystem.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TicketingSystem.Data;
    using System.Data.Entity;
    using TicketingSystem.Web.Infrastructure.Services.Contracts;
    using TicketingSystem.Web.Infrastructure.Services;
    using TicketingSystem.Web.Infrastructure.Caching;
    using TicketingSystem.Web.Infrastructure.Populators;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<DbContext>().To<TicketSystemDbContext>();
            kernel.Bind<ITicketSystemData>().To<TicketSystemData>();

            kernel.Bind<ICacheService>().To<InMemoryCache>();

            kernel.Bind<IHomeServices>().To<HomeServices>();
            kernel.Bind<IDropDownListPopulator>().To<DropDownListPopulator>();
        }        
    }
}
