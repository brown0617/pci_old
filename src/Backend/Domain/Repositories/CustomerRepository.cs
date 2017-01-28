using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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

		public Customer Save(Customer entity)
		{
			_ctx.Customers.AddOrUpdate(entity);
			_ctx.SaveChanges();
			return entity;
		}

		public void Delete(Customer entity)
		{
			throw new NotImplementedException();
		}

		public IQueryable<Customer> Get()
		{
			return _ctx.Customers;
		}

		public Customer New()
		{
			throw new NotImplementedException();
		}
	}
}