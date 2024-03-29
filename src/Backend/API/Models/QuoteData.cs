﻿using System;
using System.Collections.Generic;

namespace Backend.API.Models
{
	public class QuoteData
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
		///     Identifier of the customer that the quote is for
		/// </summary>
		public int CustomerId { get; set; }

		/// <summary>
		///     Name of Customer whom the quote is for
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		///     Identifier of Property whom the quote is for
		/// </summary>
		public int PropertyId { get; set; }

		/// <summary>
		///     Name of Property whom the quote is for
		/// </summary>
		public string PropertyName { get; set; }

		/// <summary>
		///     Int representing the status of the quote
		/// </summary>
		public int Status { get; set; }

		/// <summary>
		///     Status description of the quote
		/// </summary>
		public string StatusDesc { get; set; }

		/// <summary>
		///     Int representing the type of quote
		/// </summary>
		public int Type { get; set; }

		/// <summary>
		///     Type description of quote
		/// </summary>
		public string TypeDesc { get; set; }

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
		public int BillingStart { get; set; }

		/// <summary>
		///     Month description that billing starts
		/// </summary>
		public string BillingStartDesc { get; set; }

		/// <summary>
		///     The sales tax for the property at the time the quote was added
		/// </summary>
		public decimal SalesTaxRate { get; set; }

		/// <summary>
		///     The total amount for labor
		/// </summary>
		public decimal TotalPriceLabor { get; set; }

		/// <summary>
		///     The total amount for materials
		/// </summary>
		public decimal TotalPriceMaterials { get; set; }

		/// <summary>
		///     The total pre-tax amount for the quote
		/// </summary>
		public decimal TotalPricePretax { get; set; }

		/// <summary>
		///     The total sales tax for the quote
		/// </summary>
		public decimal SalesTaxAmount { get; set; }

		/// <summary>
		///     The total amount for the quote
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
		///     The total cost for the quote
		/// </summary>
		public decimal TotalCost { get; set; }

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
		public int BillingDay { get; set; }

		/// <summary>
		///     Billing day description (i.e. First, Fifteenth, Thirtieth)
		/// </summary>
		public string BillingDayDesc { get; set; }

		/// <summary>
		///     Percentage increase per year
		/// </summary>
		public decimal AnnualIncreasePercentage { get; set; }

		/// <summary>
		///     Int representing Season (1 or 2)
		/// </summary>
		public int Season { get; set; }

		/// <summary>
		///     Season description (Summer or Winter)
		/// </summary>
		public string SeasonDesc { get; set; }

		/// <summary>
		///     Collection of items related to quote
		/// </summary>
		public List<QuoteItemData> Items { get; set; }

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