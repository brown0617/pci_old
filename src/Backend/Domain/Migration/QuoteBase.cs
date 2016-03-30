using System;

namespace Backend.Domain.Migration
{
	public class QuoteBase
	{
		public Guid QuoteId { get; set; }
		public int DeletionStateCode { get; set; }
		public Guid AccountId { get; set; }
		public Guid ContactId { get; set; }
		public string Name { get; set; }
		public int StateCode { get; set; }
		public int StatusCode { get; set; }
	}
}