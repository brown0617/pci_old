using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Backend.Domain.Enums;

namespace Backend.Domain.Entities
{
	public class Order:IRowState
	{
		/// <summary>
		///     Identifier for the order
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Identifier for the quote that the order originated from
		/// </summary>
		public int QuoteId { get; set; }

		/// <summary>
		///     The title of the order
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		///     Identifier of the customer whom the order is for
		/// </summary>
		public int CustomerId { get; set; }

		/// <summary>
		///     Foreign key relationship to Customer
		/// </summary>
		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		/// <summary>
		///     Identifier of the property whom the order is for
		/// </summary>
		public int PropertyId { get; set; }

		/// <summary>
		///     Foreign key relationship to Property
		/// </summary>
		[ForeignKey("PropertyId")]
		public virtual Property Property { get; set; }

		/// <summary>
		///     Enum representing the type of order
		/// </summary>
		public QuoteType Type { get; set; }

		/// <summary>
		///     Type description of order
		/// </summary>
		public virtual string TypeDesc
		{
			get { return Type.ToDescription(); }
		}

		/// <summary>
		///     Base year of the Contract
		/// </summary>
		public int ContractYear { get; set; }

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
		///     The sales tax for the property at the time the order was added
		/// </summary>
		public decimal SalesTaxRate { get; set; }

		/// <summary>
		///     The total sales tax for the order
		/// </summary>
		public decimal SalesTaxAmount { get; set; }

		/// <summary>
		///     The total amount for labor
		/// </summary>
		public decimal TotalLaborPrice { get; set; }

		/// <summary>
		///     The total amount for materials
		/// </summary>
		public decimal TotalMaterialPrice { get; set; }

		/// <summary>
		///     The total pre-tax amount for the order
		/// </summary>
		public decimal TotalPricePretax { get; set; }

		/// <summary>
		///     The total cost for the order
		/// </summary>
		public decimal TotalPrice { get; set; }

		/// <summary>
		///     The total cost for labor
		/// </summary>
		public decimal TotalCostLabor { get; set; }

		/// <summary>
		///     The total cost for materials
		/// </summary>
		public decimal TotalCostMaterials { get; set; }

		/// <summary>
		///     The total cost for the order
		/// </summary>
		public decimal TotalCost { get; set; }

		/// <summary>
		///     The total estimated man hours
		/// </summary>
		public decimal TotalEstimatedManHours { get; set; }

		/// <summary>
		///     Indicates if order should include sales tax
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

		/// <summary>
		///     Collection of items related to order
		/// </summary>
		public ICollection<OrderItem> Items { get; set; }

		#region Remove after Migration

		[Column("SalesOrderId")]
		public Guid CrmSalesOrderId { get; set; }

		#endregion

		/// <summary>
		///     Date the row was created
		/// </summary>
		public DateTime CreatedOn { get; set; }

		/// <summary>
		///     Date the row was deleted
		/// </summary>
		public DateTime? DeletedOn { get; set; }

		/// <summary>
		///     Date the row was modified last
		/// </summary>
		public DateTime ModifiedOn { get; set; }
	}
}