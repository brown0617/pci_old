using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	public class WorkOrderRepository : IWorkOrderRepository
	{
		private readonly AppDbContext _ctx;

		public WorkOrderRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IEnumerable<WorkOrder> Get()
		{
			return
				_ctx.WorkOrders.Where(w => w.DeletedOn == null && w.ActualCompletion == null)
					.OrderBy(o => o.OrderItem.Order.Property.Name)
					.ThenBy(t => t.OrderItem.Service.Name)
					.ToList();
		}

		public WorkOrder Get(int id)
		{
			return _ctx.WorkOrders.First(w => w.Id == id);
		}

		public WorkOrder New()
		{
			throw new NotImplementedException();
		}

		public WorkOrder Save(WorkOrder entity)
		{
			_ctx.WorkOrders.AddOrUpdate(entity);
			_ctx.SaveChanges();
			return entity;
		}

		public void Delete(WorkOrder entity)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<WorkOrder> GetAll()
		{
			return _ctx.WorkOrders.Where(w => w.DeletedOn == null).ToList();
		}
	}
}