namespace Backend.Domain.Enums
{
	public enum ServiceFrequency
	{
		[EnumDescription("Weekly")] Weekly = 1,
		[EnumDescription("Biweekly")] Biweekly = 2,
		[EnumDescription("Monthly")] Monthly = 3,
		[EnumDescription("Annually")] Annually = 4,
		[EnumDescription("As Needed")] AsNeeded = 5
	}
}