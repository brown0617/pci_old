using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class QuoteDetailExtensionBase
	{
		[Key]
		public Guid QuoteDetailId { get; set; }

		public string New_Details { get; set; }
		public int? New_Visits { get; set; }
	}
}