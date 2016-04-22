using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class PropertyRepository : IPropertyRepository
	{
		private readonly AppDbContext _ctx;

		public PropertyRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IEnumerable<Property> FilterByCustomer(int customerId)
		{
			return
				_ctx.Quotes.Where(w => w.CustomerId == customerId)
					.Include(p => p.Property)
					//.Include(pc => pc.Property.PrimaryContact)
					.Select(s => s.Property)
					.ToList();
		}

		public IEnumerable<Property> FilterByName(string propertyName)
		{
			return _ctx.Properties.Where(w => w.Name.Contains(propertyName)).OrderBy(o => o.Name).ToList();
		}

		public IEnumerable<Property> Get()
		{
			return _ctx.Properties.ToList();
		}

		public Property Get(int id)
		{
			return _ctx.Properties.FirstOrDefault(x => x.Id == id);
		}

		public Property New()
		{
			throw new NotImplementedException();
		}

		public Property Save(Property entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Property entity)
		{
			throw new NotImplementedException();
		}
	}
}