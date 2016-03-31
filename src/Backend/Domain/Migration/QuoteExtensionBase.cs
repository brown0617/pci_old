using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class QuoteExtensionBase
	{
		[Key]
		public Guid QuoteId { get; set; }

		public int? New_QuoteType { get; set; }
		public string New_ContractYear { get; set; }
		public int? New_NumPayments { get; set; }
		public int? New_BillingStart { get; set; }
		public decimal? New_TotalAmountMaterials { get; set; }
		public decimal? New_SalesTaxPercent { get; set; }
		public decimal? New_SalesTaxAmount { get; set; }
		public decimal? New_TotalAmountQuote { get; set; }
		public decimal? New_TotalAmountPretax { get; set; }
		public decimal? New_TotalAmountLabor { get; set; }
		public decimal? New_TotalManHoursEst { get; set; }
		public bool? New_Taxable { get; set; }
		public int? New_ContractTermYears { get; set; }
		public int? New_BillingDay { get; set; }
		public decimal? New_AnnualIncrease { get; set; }
		public int? New_Season { get; set; }
	}
}