using AutoMapper;
using Backend.API;
using Backend.Domain.Repositories;
using Ninject;

namespace Backend
{
	public static class NinjectConfig
	{
		public static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			// interface bindings
			kernel.Bind<ICustomerRepository>().To<CustomerRepository>();

			// automapper binding
			var profile = new AutoMapperConfig();
			var config = new MapperConfiguration(
				c => { c.AddProfile(profile); });
			var mapper = config.CreateMapper();
			kernel.Bind<IMapper>().ToConstant(mapper);

			return kernel;
		}
	}
}