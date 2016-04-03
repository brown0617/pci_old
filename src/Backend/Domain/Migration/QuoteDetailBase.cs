using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class QuoteDetailBase
	{
		[Key]
		public Guid QuoteDetailId { get; set; }

		public Guid QuoteId { get; set; }

		[ForeignKey("QuoteId")]
		public virtual QuoteBase Quote { get; set; }

		public Guid ProductId { get; set; }

		[ForeignKey("ProductId")]
		public virtual ProductBase Product { get; set; }

		public decimal? Quantity { get; set; }
		public decimal? BaseAmount { get; set; }
		public decimal? ExtendedAmount { get; set; }
		public string Description { get; set; }
		public decimal? ManualDiscountAmount { get; set; }
	}
}