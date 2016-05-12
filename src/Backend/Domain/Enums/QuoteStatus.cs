namespace Backend.Domain.Enums
{
	public enum QuoteStatus
	{
		[EnumDescription("Open")] Open = 0,
		[EnumDescription("Won")] Won = 1,
		[EnumDescription("Lost")] Lost = 2
	}
}