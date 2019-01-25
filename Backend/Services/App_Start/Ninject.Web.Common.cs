[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Services.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Services.NinjectWebCommon), "Stop")]

namespace Services
{
    using Business;
    using Business.Helpers;
    using DataAccess;
    using DownloadUtilityLogger;
    using IBusiness;
    using IBusiness.IHelpers;
    using IDataAccess;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using System;
    using System.Web;
    using System.Web.Http;

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
            StandardKernel kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel?.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();

            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InTransientScope();

            kernel.Bind<ILogger>().To<FileLogger>().InSingletonScope();

            kernel.Bind<IIOHelper>().To<IOHelper>().InRequestScope();

            kernel.Bind<IParser>().To<SourcesParser>().InRequestScope();

            kernel.Bind<IDownloadStrategy>().To<FTPDownloadStrategy>().InThreadScope();

            kernel.Bind<IDownloadStrategy>().To<SFTPDownloadStrategy>().InThreadScope();

            kernel.Bind<IDownloadStrategy>().To<UriDownloadStrategy>().InThreadScope();

            kernel.Bind<IDownloadStrategyFactory>().To<DownloadStrategyFactory>().InRequestScope();

            kernel.Bind<IDownloadManager>().To<DownloadManager>().InRequestScope();

        }
    }
}