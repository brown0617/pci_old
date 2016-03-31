using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class QuoteBase
	{
		[Key]
		public Guid QuoteId { get; set; }
		public int DeletionStateCode { get; set; }
		public Guid? AccountId { get; set; }
		public Guid? ContactId { get; set; }
		public string Name { get; set; }
		public int StateCode { get; set; }
		public int StatusCode { get; set; }
	}
}