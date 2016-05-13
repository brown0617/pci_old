using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[RoutePrefix("api/orders")]
	[ExceptionHandling]
	public class OrdersController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IOrderRepository _repository;

		public OrdersController(IOrderRepository orderRepository, IMapper mapper)
		{
			_repository = orderRepository;
			_mapper = mapper;
		}

		public IEnumerable<OrderData> Get()
		{
			var orderData = new List<OrderData>();
			_mapper.Map(_repository.Get(), orderData);
			return orderData.OrderBy(x => x.Property.Name).ThenByDescending(x => x.ContractYear);
		}

		public OrderData Get(int id)
		{
			var orderData = new OrderData();
			_mapper.Map(_repository.Get(id), orderData);
			return orderData;
		}

		public OrderData Put([FromBody] OrderData orderData)
		{
			var order = _mapper.Map(orderData, new Order());
			return _mapper.Map(_repository.Save(order), orderData);
		}

		[Route("new")]
		public OrderData GetNew()
		{
			var orderData = new OrderData();
			_mapper.Map(_repository.New(), orderData);
			return orderData;
		}
	}
}