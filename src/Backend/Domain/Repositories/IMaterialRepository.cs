using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IMaterialRepository : IRepository<Material, int>
	{
		IEnumerable<Material> FilterByName(string materialName);
	}
}