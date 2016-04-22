using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Backend.Domain.Entities;
using Backend.Domain.Enums;

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
			return _ctx.Quotes.Include("Property").Include("Customer").ToList();
		}

		public Quote Get(int id)
		{
			return _ctx.Quotes.Include(c => c.Property).Include(i => i.Items).FirstOrDefault(x => x.Id == id);
		}

		public Quote New()
		{
			return new Quote();
		}

		public Quote Save(Quote entity)
		{
			// add/update quote
			_ctx.Quotes.AddOrUpdate(entity);
			_ctx.SaveChanges();

			// add/update quote items
			if (entity.Type == QuoteType.Installment)
			{
				_ctx.Services.Where(w => w.CompleteCare).ToList().ForEach(service => _ctx.QuoteItems.Add(new QuoteItem
				{
					QuoteId = entity.Id,
					ServiceId = service.Id,
					ServiceCost = service.Cost,
					ServicePrice = service.Price,
					ServiceQuantity = 1,
					ServiceUnitCost = service.Cost,
					ServiceUnitPrice = service.Price
				}));
			}
			else
			{
				entity.Items.ToList().ForEach(item => _ctx.QuoteItems.AddOrUpdate(item));
			}

			// save changes
			_ctx.SaveChanges();
			return entity;
		}

		public void Delete(Quote entity)
		{
			throw new NotImplementedException();
		}
	}
}