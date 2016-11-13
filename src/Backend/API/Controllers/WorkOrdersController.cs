using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Backend.API.Models;
using Backend.Domain.Entities;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[RoutePrefix("api/workOrders")]
	[ExceptionHandling]
	public class WorkOrdersController : ApiController
	{
		private readonly IMapper _mapper;
		private readonly IWorkOrderRepository _repository;

		public WorkOrdersController(IWorkOrderRepository workOrderRepository, IMapper mapper)
		{
			_mapper = mapper;
			_repository = workOrderRepository;
		}

		[Route("active")]
		public IEnumerable<WorkOrderData> GetAllActive()
		{
			var workOrderData = new List<WorkOrderData>();
			_mapper.Map(_repository.Get().Where(x => x.ActualCompletion == null), workOrderData);
			return workOrderData;
		}

		public IEnumerable<WorkOrderData> Get()
		{
			var workOrderData = new List<WorkOrderData>();
			_mapper.Map(_repository.Get(), workOrderData);
			return workOrderData;
		}

		public WorkOrderData Get(int id)
		{
			var workOrderData = new WorkOrderData();
			_mapper.Map(_repository.Get(id), workOrderData);
			return workOrderData;
		}

		public WorkOrderData Put([FromBody] WorkOrderData workOrderData)
		{
			var workOrder = _mapper.Map(workOrderData, new WorkOrder());
			return _mapper.Map(_repository.Save(workOrder), workOrderData);
		}
	}
}