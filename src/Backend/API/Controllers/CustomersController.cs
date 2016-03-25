using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
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

		public void Put([FromBody] CustomerData customerData)
		{
			var entityType = Type.GetType(customerData.Type);
			if (entityType == null) return;
			var entity = Activator.CreateInstance(entityType) as Customer;
			var customer = _mapper.Map(customerData, entity);
			_repository.Save(customer);
		}
	}
}