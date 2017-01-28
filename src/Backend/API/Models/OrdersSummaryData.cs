using Backend.Domain.Enums;

namespace Backend.API.Models
{
	public class OrdersSummaryData
	{
		/// <summary>
		///     Season description (Summer or Winter)
		/// </summary>
		public string SeasonDesc { get; set; }

		/// <summary>
		///     Total number of orders
		/// </summary>
		public int OrderCount { get; set; }

		/// <summary>
		///     Total Cost of orders
		/// </summary>
		public decimal TotalCost { get; set; }

		/// <summary>
		///     Total Price of orders
		/// </summary>
		public decimal TotalPrice { get; set; }

		/// <summary>
		///     Gross Profit of orders
		/// </summary>
		public decimal GrossProfit { get; set; }

		/// <summary>
		///     Season
		/// </summary>
		public Season Season { get; set; }
	}
}