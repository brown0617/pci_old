using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class MaterialRepository : IMaterialRepository
	{
		private readonly AppDbContext _ctx;

		public MaterialRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IQueryable<Material> Get()
		{
			throw new NotImplementedException();
		}

		public Material Get(int id)
		{
			throw new NotImplementedException();
		}

		public Material New()
		{
			throw new NotImplementedException();
		}

		public Material Save(Material entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(Material entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Material> FilterByName(string materialName)
		{
			return _ctx.Materials.Where(w => w.Name.Contains(materialName)).OrderBy(o => o.Name).ToList();
		}
	}
}