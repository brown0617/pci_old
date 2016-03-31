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
				_ctx.Properties.Where(w => w.CustomerId == customerId)
					.Include(c => c.Customer)
					.Include(pc => pc.PrimaryContact)
					.ToList();
		}

		public IEnumerable<Property> Get()
		{
			return _ctx.Properties.Include(c => c.Customer).ToList();
		}

		public Property Get(int id)
		{
			return _ctx.Properties.FirstOrDefault(x => x.Id == id);
		}

		public Property New()
		{
			throw new NotImplementedException();
		}

		public void Save(Property entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Property entity)
		{
			throw new NotImplementedException();
		}
	}
}