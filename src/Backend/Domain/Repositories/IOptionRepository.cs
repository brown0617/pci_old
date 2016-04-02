using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IOptionRepository
	{
		IEnumerable<Option> Get(string tEnum);
	}
}