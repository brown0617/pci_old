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
			return
				_ctx.Quotes.Where(w => w.DeletedOn == null).Include("Property").Include("Customer").ToList();
		}

		public Quote Get(int id)
		{
			var quote =
				_ctx.Quotes.Where(x => x.Id == id).Include(c => c.Property)
					.Select(q => new
					{
						quote = q,
						items = q.Items.Where(w => w.DeletedOn == null)
					});

			return quote.AsEnumerable().Select(s => s.quote).FirstOrDefault();
		}

		public Quote New()
		{
			return new Quote {CreatedOn = DateTime.UtcNow};
		}

		public Quote Save(Quote entity)
		{
			// add/update quote
			_ctx.Quotes.AddOrUpdate(entity);
			_ctx.SaveChanges();

			// add/update quote items
			if (entity.Type == QuoteType.Installment && !entity.Items.Any())
			{
				_ctx.Services.Where(w => w.CompleteCare && w.Season == entity.Season).ToList().ForEach(service => _ctx.QuoteItems.Add(new QuoteItem
				{
					QuoteId = entity.Id,
					ServiceId = service.Id,
					ServiceCost = service.Cost,
					ServicePrice = service.Price,
					ServiceQuantity = 1,
					ServiceUnitCost = service.Cost,
					ServiceUnitPrice = service.Price,
					Description = service.Description
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

		public Quote Close(Quote quote, bool createOrder)
		{
			throw new NotImplementedException();
		}
	}
}