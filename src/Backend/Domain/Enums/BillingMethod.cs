namespace Backend.Domain.Enums
{
	public enum BillingMethod
	{
		[EnumDescription("Per Visit")] PerVisit = 1,
		[EnumDescription("Fixed Price")] FixedPrice = 2,
		[EnumDescription("Job Completion")] JobCompletion = 3,
		[EnumDescription("See Quote")] SeeQuote = 4,
		[EnumDescription("Per Hour")] PerHour = 5,
		[EnumDescription("Do Not Bill")] DoNotBill = 6
	}
}