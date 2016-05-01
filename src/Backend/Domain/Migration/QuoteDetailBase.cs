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

		public Guid? ProductId { get; set; }
		public decimal? Quantity { get; set; }
		public decimal? PricePerUnit { get; set; }
		public decimal? ExtendedAmount { get; set; }
		public string Description { get; set; }
		public decimal? ManualDiscountAmount { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}