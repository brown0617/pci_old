using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IPropertyRepository : IRepository<Property, int>
	{
		IEnumerable<Property> FilterByCustomer(int customerId);
		IEnumerable<Property> FilterByName(string propertyName);
	}
}