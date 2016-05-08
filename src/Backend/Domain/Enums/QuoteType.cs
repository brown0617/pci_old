namespace Backend.Domain.Enums
{
	public enum QuoteType
	{
		[EnumDescription("Complete Care")] Installment = 1,
		[EnumDescription("Landscape Project")] LandscapeProject = 3,
		[EnumDescription("Maintenance")] Maintenence = 2,
		[EnumDescription("Construction Project")] ConstructionProject = 4
	}
}