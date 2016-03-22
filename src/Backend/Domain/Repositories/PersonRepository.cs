using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class PersonRepository : IPersonRepository
	{
		private readonly AppDbContext _ctx;

		public PersonRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IEnumerable<Person> Get()
		{
			return _ctx.People;
		}

		public Person Get(int id)
		{
			return _ctx.People.FirstOrDefault(x => x.Id == id);
		}

		public void Save(Person entity)
		{
			_ctx.People.AddOrUpdate(entity);
			_ctx.SaveChanges();
		}

		public void Delete(Person entity)
		{
			throw new NotImplementedException();
		}

		public Person New()
		{
			return new Person();
		}
	}
}