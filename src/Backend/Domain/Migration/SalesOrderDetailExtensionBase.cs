using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class SalesOrderDetailExtensionBase
	{
		[Key]
		public Guid SalesOrderDetailId { get; set; }
		public string New_Details { get; set; }
		public int? New_Visits { get; set; }
		public int? New_BillingMethod { get; set; }
		public int? New_NumPayments { get; set; }
		public int? New_BillingStart { get; set; }
		public Guid? New_MaterialsId { get; set; }
		public decimal? New_PricePerUnit_Materials { get; set; }
		public decimal? New_QuantityMaterials { get; set; }
		public DateTime? New_ServiceDeadline { get; set; }
		public int? New_ServiceFrequency { get; set; }
	}
}