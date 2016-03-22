using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly AppDbContext _ctx;

		public CustomerRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public Customer Get(int id)
		{
			return _ctx.Customers.FirstOrDefault(x => x.Id == id);
		}

		public void Save(Customer entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Customer entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Customer> Get()
		{
			return _ctx.Customers;
		}

		public Customer New()
		{
			throw new NotImplementedException();
		}
	}
}