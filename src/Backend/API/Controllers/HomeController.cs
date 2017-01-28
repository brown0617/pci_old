using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Backend.API.Models;
using Backend.Domain.Enums;
using Backend.Domain.Repositories;

namespace Backend.API.Controllers
{
	[RoutePrefix("api/home")]
	[ExceptionHandling]
	public class HomeController : ApiController
	{
		private readonly IOrderRepository _repository;

		public HomeController(IOrderRepository orderRepository)
		{
			_repository = orderRepository;
		}

		[Route("ordersSummary/{contractYear}")]
		public IEnumerable<OrdersSummaryData> GetOrdersSummary(string contractYear)
		{
			var ordersSummaryData=
				_repository.Get()
					.Where(w => w.ContractYear == contractYear)
					.GroupBy(g => g.Season)
					.Select(
						o =>
							new OrdersSummaryData
							{
								Season = o.Key,
								OrderCount = o.Count(),
								TotalCost = o.Sum(w => w.TotalCost),
								TotalPrice = o.Sum(w => w.TotalPrice),
								GrossProfit = o.Sum(w => w.TotalPrice) - o.Sum(w => w.TotalCost)
							})
					.ToList();

			ordersSummaryData.ForEach(
				r => r.SeasonDesc = r.Season.ToDescription()
			);

			return ordersSummaryData;
		}
	}
}