namespace Backend.Domain.Enums
{
	public enum BillingMethod
	{
		[EnumDescription("Upon Completion")] UponCompletion = 1,
		[EnumDescription("Monthly")] Monthly = 2,
		[EnumDescription("Weekly")] Weekly = 3,
		[EnumDescription("See Quote")] SeeQuote = 4
	}
}