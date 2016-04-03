using System;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Domain.Enums;

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
		///     Identifier of Property whom the quote is for
		/// </summary>
		public int PropertyId { get; set; }

		/// <summary>
		///     Foreign key relationship to Property
		/// </summary>
		[ForeignKey("PropertyId")]
		public virtual Property Property { get; set; }

		/// <summary>
		///     Enum representing the status of the quote
		/// </summary>
		public QuoteStatus Status { get; set; }

		/// <summary>
		///     Status description of the quote
		/// </summary>
		public virtual string StatusDesc
		{
			get { return Status.ToDescription(); }
		}

		/// <summary>
		///     Enum representing the type of quote
		/// </summary>
		public QuoteType Type { get; set; }

		/// <summary>
		///     Type description of quote
		/// </summary>
		public virtual string TypeDesc
		{
			get { return Type.ToDescription(); }
		}

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
		public Month BillingStart { get; set; }

		/// <summary>
		///     Month description that billing starts
		/// </summary>
		public virtual string BillingStartDesc
		{
			get { return BillingStart.ToDescription(); }
		}

		/// <summary>
		///     The total amount for materials
		/// </summary>
		public decimal TotalAmountMaterials { get; set; }

		/// <summary>
		///     The total sales tax for the quote
		/// </summary>
		public decimal SalesTaxAmount { get; set; }

		/// <summary>
		///     The total amount for the quote
		/// </summary>
		public decimal TotalAmountQuote { get; set; }

		/// <summary>
		///     The total pre-tax amount for the quote
		/// </summary>
		public decimal TotalAmountPretax { get; set; }

		/// <summary>
		///     The total amount for labor
		/// </summary>
		public decimal TotalAmountLabor { get; set; }

		/// <summary>
		///     The total estimated man hours
		/// </summary>
		public decimal TotalEstimatedManHours { get; set; }

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
		public BillingDay BillingDay { get; set; }

		/// <summary>
		///     Billing day description (i.e. First, Fifteenth, Thirtieth)
		/// </summary>
		public virtual string BillingDayDesc { get; set; }

		/// <summary>
		///     Percentage increase per year
		/// </summary>
		public decimal AnnualIncreasePercentage { get; set; }

		/// <summary>
		///     Enum representing Season (1 or 2)
		/// </summary>
		public Season Season { get; set; }

		/// <summary>
		///     Season description (Summer or Winter)
		/// </summary>
		public virtual string SeasonDesc
		{
			get { return Season.ToDescription(); }
		}

		#region Remove after Migration

		[Column("QuoteId")]
		public Guid CrmQuoteId { get; set; }

		#endregion
	}
}