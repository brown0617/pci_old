namespace Backend.API.Models
{
	public class CustomerData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BillingAddressStreet1 { get; set; }
		public string BillingAddressStreet2 { get; set; }
		public string BillingAddressCity { get; set; }
		public string BillingAddressState { get; set; }
		public string BillingAddressZip { get; set; }
		public string Terms { get; set; }
		public string Type { get; set; }
	}
}