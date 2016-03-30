using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class Quote
	{
		/// <summary>
		///     Identifier for the quote
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     The title of the quote
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Identifier of Customer whom the quote is for
		/// </summary>
		public int CustomerId { get; set; }

		/// <summary>
		///     Foreign key relationship to Customer
		/// </summary>
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		/// <summary>
		///     Enum representing the status of the quote
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		///     Enum representing the type of quote
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		///     Base year of the Contract
		/// </summary>
		public string ContractYear { get; set; }

		/// <summary>
		///     Number of payments
		/// </summary>
		public int NumberOfPayments { get; set; }

		/// <summary>
		///     Month that billing starts
		/// </summary>
		public int BillingStart { get; set; }

		/// <summary>
		///     The total amount for materials
		/// </summary>
		public double TotalAmountMaterials { get; set; }

		/// <summary>
		///     The total sales tax for the quote
		/// </summary>
		public double SalesTaxAmount { get; set; }

		/// <summary>
		///     The total amount for the quote
		/// </summary>
		public double TotalAmountQuote { get; set; }

		/// <summary>
		///     The total pre-tax amount for the quote
		/// </summary>
		public double TotalAmountPretax { get; set; }

		/// <summary>
		///     The total amount for labor
		/// </summary>
		public double TotalAmountLabor { get; set; }

		/// <summary>
		///     The total estimated man hours
		/// </summary>
		public float TotalEstimatedManHours { get; set; }

		/// <summary>
		///     Indicates if quote should include sales tax
		/// </summary>
		public bool Taxable { get; set; }

		/// <summary>
		///     Contract term in years
		/// </summary>
		public int ContractTermYears { get; set; }

		/// <summary>
		///     Billing day (i.e. 1, 15, 30)
		/// </summary>
		public int BillingDay { get; set; }

		/// <summary>
		///     Percentage increase per year
		/// </summary>
		public float AnnualIncreasePercentage { get; set; }

		/// <summary>
		///     Enum representing Season (Summer or Winter)
		/// </summary>
		public int Season { get; set; }
	}
}