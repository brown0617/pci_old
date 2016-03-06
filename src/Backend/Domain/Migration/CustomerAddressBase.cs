using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class CustomerAddressBase
	{
		[Key]
		public Guid CustomerAddressId { get; set; }
		
		[Column("ParentId")]
		public Guid AccountId { get; set; }
		public int AddressNumber { get; set; }
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string City { get; set; }
		public string StateOrProvince { get; set; }
		public string PostalCode { get; set; }
	}
}