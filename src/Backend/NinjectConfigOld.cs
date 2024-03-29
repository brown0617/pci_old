﻿using AutoMapper;
using Backend.API;
using Backend.Domain.Repositories;
using Ninject;

namespace Backend
{
	public static class NinjectConfigOld
	{
		public static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

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