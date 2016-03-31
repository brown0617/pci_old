namespace Backend.Domain.Enums
{
	public enum QuoteType
	{
		[EnumDescription("Complete Care")] Installment = 1,
		[EnumDescription("Maintenance")] Monthly = 2,
		[EnumDescription("Project")] OneTime = 3
	}
}