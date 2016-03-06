using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;
using NLog;

namespace Backend.API.Controllers
{
	[EnableCors("http://localhost:9000", "*", "get,post")]
	[ExceptionHandling]
	public class CustomersController : ApiController
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();
		private readonly IMapper _mapper;
		private readonly ICustomerRepository _repository;

		public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
		{
			_repository = customerRepository;
			_mapper = mapper;
		}

		public IEnumerable<CustomerData> Get()
		{
			var customerData = new List<CustomerData>();
			_mapper.Map(_repository.Get(), customerData);
			return customerData;
		}

		public CustomerData Get(int id)
		{
			var customerData = new CustomerData();
			_mapper.Map(_repository.Get(id), customerData);
			return customerData;
		}
	}
}