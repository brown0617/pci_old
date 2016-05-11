using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class SalesOrderExtensionBase
	{
		[Key]
		public Guid SalesOrderId { get; set; }

		public int? New_OrderType { get; set; }
		public string New_ContractYear { get; set; }
		public int? New_NumPayments { get; set; }
		public int? New_BillingStart { get; set; }
		public decimal? New_TotalAmountMaterials { get; set; }
		public decimal? New_SalesTaxPercent { get; set; }
		public decimal? New_SalesTaxAmount { get; set; }
		public decimal? New_TotalAmountOrder { get; set; }
		public decimal? New_TotalAmountPretax { get; set; }
		public decimal? New_TotalAmountLabor { get; set; }
		public decimal? New_TotalManHoursEst { get; set; }
		public bool? New_Taxable { get; set; }
		public int? New_BillingDay { get; set; }
		public string New_PONumber { get; set; }
		public int? New_Season { get; set; }
	}
}