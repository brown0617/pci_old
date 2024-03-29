﻿using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class ServiceRepository : IServiceRepository
	{
		private readonly AppDbContext _ctx;

		public ServiceRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IQueryable<Service> Get()
		{
			return _ctx.Services.OrderBy(o => o.Name);
		}

		public Service Get(int id)
		{
			throw new NotImplementedException();
		}

		public Service New()
		{
			throw new NotImplementedException();
		}

		public Service Save(Service entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Service entity)
		{
			throw new NotImplementedException();
		}
	}
}