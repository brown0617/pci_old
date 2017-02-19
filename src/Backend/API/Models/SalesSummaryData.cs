using System.Collections.Generic;

namespace Backend.API.Models
{
	public class SalesSummaryData
	{
		/// <summary>
		///     Order Year
		/// </summary>
		public string Year { get; set; }

		/// <summary>
		///     List of orders by season
		/// </summary>
		public List<OrdersBySeasonData> OrdersBySeason { get; set; }
	}
}