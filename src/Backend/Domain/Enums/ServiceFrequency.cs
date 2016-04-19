namespace Backend.Domain.Enums
{
	public enum ServiceFrequency
	{
		[EnumDescription("Weekly")] Weekly = 1,
		[EnumDescription("Monthly")] Monthly = 2,
		[EnumDescription("Annually")] Annually = 3,
		[EnumDescription("As Needed")] AsNeeded = 4
	}
}