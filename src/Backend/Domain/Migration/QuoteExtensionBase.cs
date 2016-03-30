using System;

namespace Backend.Domain.Migration
{
	public class QuoteExtensionBase
	{
		public Guid QuoteId { get; set; }
		public int New_QuoteType { get; set; }
		public string New_ContractYear { get; set; }
		public int New_NumPayments { get; set; }
		public int New_BillingStart { get; set; }
		public double New_TotalAmountMaterials { get; set; }
		public double new_totalamountmaterials_Base { get; set; }
		public float New_SalesTaxPercent { get; set; }
		public double New_SalesTaxAmount { get; set; }
		public double new_salestaxamount_Base { get; set; }
		public double New_TotalAmountQuote { get; set; }
		public double new_totalamountquote_Base { get; set; }
		public double New_TotalAmountPretax { get; set; }
		public double new_totalamountpretax_Base { get; set; }
		public double New_TotalAmountLabor { get; set; }
		public double new_totalamountlabor_Base { get; set; }
		public float New_TotalManHoursEst { get; set; }
		public bool New_Taxable { get; set; }
		public int New_ContractTermYears { get; set; }
		public int New_BillingDay { get; set; }
		public float New_AnnualIncrease { get; set; }
		public int New_Season { get; set; }
	}
}