namespace Backend.Domain.Enums
{
	public enum QuoteStatus
	{
		[EnumDescription("Active")] Active = 0,
		[EnumDescription("Won")] Won = 1,
		[EnumDescription("Lost")] Lost = 2
	}
}