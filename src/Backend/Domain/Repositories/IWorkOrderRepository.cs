using System.Collections.Generic;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public interface IWorkOrderRepository : IRepository<WorkOrder, int>
	{
	}
}