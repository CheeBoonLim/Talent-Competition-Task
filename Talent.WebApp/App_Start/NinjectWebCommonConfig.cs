using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Talent.Data;
using Talent.Service.Domain;
using Talent.Service.Persons;
using Talent.Service.Security;
using Talent.Service.SendMail;
using Talent.Service.SignUp;
using Talent.Service.Validation;
using Talent.Service.Validation.ModelValidation;
using System;
using System.Web;
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Talent.WebApp.App_Start.NinjectWebCommonConfig), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Talent.WebApp.App_Start.NinjectWebCommonConfig), "Stop")]
namespace Talent.WebApp.App_Start
{
    public class NinjectWebCommonConfig
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
            kernel.Bind<IDbContext>().To<TalentDbContext>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            kernel.Bind(typeof(IValidationService<>)).To(typeof(ValidationService<>));
            kernel.Bind(typeof(IModelValidationService<,>)).To(typeof(ModelValidationService<,>));
            kernel.Bind<IUserState>().To<UserState>();
            kernel.Bind<IHttpContextFactory>().To<HttpContextFactory>();
            kernel.Bind<IApplicationContext>().To<ApplicationContext>();
            kernel.Bind<IPasswordStorage>().To<PasswordStorage>();
            kernel.Bind<ISignUpService>().To<SignUpService>();
            kernel.Bind<IEmailService>().To<EmailService>();
            kernel.Bind<IPersonService>().To<PersonService>();
        }
    }
}