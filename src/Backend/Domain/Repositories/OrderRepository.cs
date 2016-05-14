using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Backend.Domain.Entities;

namespace Backend.Domain.Repositories
{
	internal class OrderRepository : IOrderRepository
	{
		private readonly AppDbContext _ctx;

		public OrderRepository(AppDbContext appDbContext)
		{
			_ctx = appDbContext;
		}

		public IEnumerable<Order> Get()
		{
			return _ctx.Orders.Where(w => w.DeletedOn == null).ToList();
		}

		public Order Get(int id)
		{
			return _ctx.Orders.FirstOrDefault(w => w.Id == id);
		}

		public Order New()
		{
			return new Order();
		}

		public Order Save(Order entity)
		{
			var newOrder = entity.Id == 0;

			_ctx.Orders.AddOrUpdate(entity);
			_ctx.SaveChanges();

			if (newOrder)
				// create work order records
				entity.Items.ToList().ForEach(x =>
					_ctx.WorkOrders.Add(new WorkOrder
					{
						OrderItemId = x.Id,
						Details = x.Description,
						VisitNumber = 1,
						ScheduledCompletion = x.ServiceDeadline
					})
					);

			_ctx.SaveChanges();

			return entity;
		}

		public void Delete(Order entity)
		{
			throw new NotImplementedException();
		}
	}
}