using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class QuoteRepository : IQuoteRepository
	{
		private readonly AppDbContext _ctx;

		public QuoteRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IEnumerable<Quote> Get()
		{
			return _ctx.Quotes.Include("Property").Include("Property.Customer").ToList();
		}

		public Quote Get(int id)
		{
			return _ctx.Quotes.Include(c => c.Property).FirstOrDefault(x => x.Id == id);
		}

		public Quote New()
		{
			throw new NotImplementedException();
		}

		public void Save(Quote entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Quote entity)
		{
			throw new NotImplementedException();
		}
	}
}