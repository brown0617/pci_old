using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class AccountBase
	{
		[Key]
		public Guid AccountId { get; set; }

		public string Name { get; set; }
		public string AccountNumber { get; set; }
		public Guid? ParentAccountId { get; set; }

		public Guid? PrimaryContactId { get; set; }
		[ForeignKey("PrimaryContactId")]
		public virtual ContactBase Contact { get; set; }
		
		public ICollection<CustomerAddressBase> Addresses { get; set; }
	}
}