using Backend.Domain.Enums;

namespace Backend.API.Models
{
	public class OrdersBySeasonData
	{
		/// <summary>
		///     Season description (Summer or Winter)
		/// </summary>
		public string SeasonDesc { get; set; }

		/// <summary>
		///     Total Price of current year orders
		/// </summary>
		public decimal TotalPriceCurrent { get; set; }

		/// <summary>
		///     Total Price of prior year orders
		/// </summary>
		public decimal TotalPricePrior { get; set; }

		/// <summary>
		///     Total Price of all prior year orders
		/// </summary>
		public decimal TotalPriceAllPrior { get; set; }

		/// <summary>
		///     Average Price of all prior year orders
		/// </summary>
		public decimal AveragePriceAllPrior { get; set; }

		/// <summary>
		///     Season
		/// </summary>
		public Season Season { get; set; }
	}
}