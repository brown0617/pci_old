using System.Reflection;
using System.Web.Http;
using AutoMapper;
using Backend.API;
using Backend.Authentication.Config;
using Backend.Authentication.Models;
using Backend.Authentication.Repositories;
using Backend.Domain;
using Backend.Domain.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace Backend
{
	public partial class Startup
	{
		public IKernel ConfigureNinject(IAppBuilder app, HttpConfiguration config)
		{
			var kernel = CreateKernel();
			app.UseNinjectMiddleware(() => kernel)
				.UseNinjectWebApi(config);

			return kernel;
		}

		public IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			kernel.Load(Assembly.GetExecutingAssembly());
			return kernel;
		}
	}

	public class NinjectConfig : NinjectModule
	{
		public override void Load()
		{
			RegisterServices(Kernel);
		}

		private void RegisterServices(IBindingRoot kernel)
		{
			//kernel.Bind<AppDbContext>().ToSelf().InRequestScope();
			kernel.Bind<AuthContext>().ToConstructor(_ => new AuthContext());
			kernel.Bind<AppDbContext>().ToConstructor(_ => new AppDbContext());

			// auth bindings
			kernel.Bind<IOAuthAuthorizationServerOptions>()
				.To<AppOAuthAuthorizationServerOptions>();
			kernel.Bind<IOAuthAuthorizationServerProvider>()
				.To<AppOAuthAuthorizationServerProvider>();
			kernel.Bind<IAuthenticationTokenProvider>()
				.To<AppAuthenticationTokenProvider>();

			kernel.Bind<IUserStore<IdentityUser>>().To<UserStore<IdentityUser>>();
			//kernel.Bind<UserManager<AppUser>>().ToSelf();

			// interface bindings
			kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
			kernel.Bind<IMaterialRepository>().To<MaterialRepository>();
			kernel.Bind<IOptionRepository>().To<OptionRepository>();
			kernel.Bind<IOrderRepository>().To<OrderRepository>();
			kernel.Bind<IPersonRepository>().To<PersonRepository>();
			kernel.Bind<IPropertyRepository>().To<PropertyRepository>();
			kernel.Bind<IQuoteRepository>().To<QuoteRepository>();
			kernel.Bind<IServiceRepository>().To<ServiceRepository>();
			kernel.Bind<IWorkOrderRepository>().To<WorkOrderRepository>();
			kernel.Bind<IAuthRepository>().To<AuthRepository>();

			//kernel.Bind(x =>
			//{
			//	x.FromThisAssembly()
			//	.SelectAllClasses()
			//	.BindDefaultInterface();
			//});

			// automapper binding
			var profile = new AutoMapperConfig();
			var config = new MapperConfiguration(
				c => { c.AddProfile(profile); });
			var mapper = config.CreateMapper();
			kernel.Bind<IMapper>().ToConstant(mapper);
		}
	}
}