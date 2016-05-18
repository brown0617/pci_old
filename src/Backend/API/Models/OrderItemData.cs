using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.API.Models
{
	public class OrderItemData
	{
		public int Id { get; set; }
		public int OrderId { get; set; }

		[ForeignKey("OrderId")]
		public virtual OrderDataNoItems Order { get; set; }

		public string Description { get; set; }
		public int? ServiceId { get; set; }

		[ForeignKey("ServiceId")]
		public virtual ServiceData Service { get; set; }

		public decimal ServiceQuantity { get; set; }
		public decimal ServiceUnitCost { get; set; }
		public decimal ServiceCost { get; set; }
		public decimal ServiceUnitPrice { get; set; }
		public decimal ServicePrice { get; set; }
		public int? MaterialId { get; set; }

		[ForeignKey("MaterialId")]
		public virtual MaterialData Material { get; set; }

		public decimal MaterialQuantity { get; set; }
		public decimal MaterialUnitCost { get; set; }
		public decimal MaterialCost { get; set; }
		public decimal MaterialUnitPrice { get; set; }
		public decimal MaterialPrice { get; set; }
		public decimal ManualDiscountAmount { get; set; }
		public int Visits { get; set; }
		public int? BillingMethod { get; set; }
		public int NumberOfPayments { get; set; }
		public int? BillingStart { get; set; }
		public DateTime? ServiceDeadline { get; set; }
		public int? ServiceFrequency { get; set; }

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