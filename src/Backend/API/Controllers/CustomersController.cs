using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[ExceptionHandling]
	public class CustomersController : ApiController
	{
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
			return customerData.OrderBy(x => x.Name);
		}

		public CustomerData Get(int id)
		{
			var customerData = new CustomerData();
			_mapper.Map(_repository.Get(id), customerData);
			return customerData;
		}
	}
}