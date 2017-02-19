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

		[Route("salesSummary/{contractYear}")]
		public IEnumerable<OrdersBySeasonData> GetSalesSummary(int contractYear)
		{
			var priorYear = contractYear - 1;
			var ordersSummaryData = _repository.Get()
				.Where(w => w.ContractYear <= contractYear)
				.GroupBy(g => g.Season)
				.Select(
					o =>
						new OrdersBySeasonData
						{
							Season = o.Key,
							TotalPriceCurrent = o.Sum(w => w.ContractYear == contractYear ? w.TotalPrice : 0),
							TotalPricePrior = o.Sum(w => w.ContractYear == priorYear ? w.TotalPrice : 0),
							TotalPriceAllPrior = o.Sum(w => w.ContractYear < contractYear ? w.TotalPrice : 0)
						})
				.ToList();

			var priorYears = _repository.Get()
				.Where(w => w.ContractYear <= contractYear)
				.GroupBy(g => g.Season)
				.Select(o => new
				{
					Season = o.Key,
					YearCount = o.Select(l => l.ContractYear).Distinct().Count()
				}).ToLookup(item => item.Season);

			foreach (var ordersBySeasonData in ordersSummaryData)
			{
				ordersBySeasonData.SeasonDesc = ordersBySeasonData.Season.ToDescription();

				var firstOrDefault = priorYears[ordersBySeasonData.Season].FirstOrDefault();
				if (firstOrDefault != null)
				{
					var priorYearsCount = firstOrDefault.YearCount;

					ordersBySeasonData.AveragePriceAllPrior = ordersBySeasonData.TotalPriceAllPrior/priorYearsCount;
				}
			}

			return ordersSummaryData;
		}
	}
}