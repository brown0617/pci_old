namespace Backend.Domain.Enums
{
	public enum QuoteType
	{
		[EnumDescription("Complete Care")] Installment = 1,
		[EnumDescription("Construction Project")] ConstructionProject = 2,
		[EnumDescription("Landscape Project")] LandscapeProject = 3,
		[EnumDescription("Maintenance")] Maintenence = 4
	}
}