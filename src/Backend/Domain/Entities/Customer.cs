using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public abstract class Customer
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string BillingAddressStreet1 { get; set; }
		public string BillingAddressStreet2 { get; set; }
		public string BillingAddressCity { get; set; }
		public string BillingAddressState { get; set; }
		public string BillingAddressZip { get; set; }
		public float SalesTaxPercent { get; set; }
		public string Terms { get; set; }
		public Guid CrmAccountId { get; set; }
	}

	public class ResidentialCustomer : Customer
	{
		public new string Name
		{
			get { return Title + FirstName; }
		}

		public string Title { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
	}

	public class CommercialCustomer : Customer
	{
		public string AccountNumber { get; set; }
		public int? PrimaryContactId { get; set; }

		[ForeignKey("PrimaryContactId")]
		public virtual Person Person { get; set; }
	}
}