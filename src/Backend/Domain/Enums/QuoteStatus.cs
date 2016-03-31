namespace Backend.Domain.Enums
{
	public enum QuoteStatus
	{
		[EnumDescription("Draft")] Draft = 0,
		[EnumDescription("Active")] Active = 1,
		[EnumDescription("Closed")] Closed = 2
	}
}