using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class QuoteItem
	{
		public int Id { get; set; }
		public int QuoteId { get; set; }

		[ForeignKey("QuoteId")]
		public virtual Quote Quote { get; set; }

		public string Description { get; set; }
		public int? ServiceId { get; set; }

		[ForeignKey("ServiceId")]
		public virtual Service Service { get; set; }

		public decimal ServiceQuantity { get; set; }
		public decimal ServiceUnitPrice { get; set; }
		public decimal ServicePrice { get; set; }
		public int? MaterialId { get; set; }

		[ForeignKey("MaterialId")]
		public virtual Material Material { get; set; }

		public decimal MaterialQuantity { get; set; }
		public decimal MaterialUnitPrice { get; set; }
		public decimal MaterialPrice { get; set; }
		public decimal ManualDiscountAmount { get; set; }
		public int Visits { get; set; }
		public int? BillingMethod { get; set; }
		public int NumberOfPayments { get; set; }
		public int? BillingStart { get; set; }
		public DateTime? ServiceDeadline { get; set; }
		public int? ServiceFrequency { get; set; }
	}
}