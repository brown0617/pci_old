namespace Backend.Domain.Enums
{
	public enum QuoteType
	{
		[EnumDescription("Complete Care")] Installment = 0,
		[EnumDescription("Maintenance")] Monthly = 1,
		[EnumDescription("Project")] OneTime = 2
	}
}