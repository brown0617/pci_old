using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class ContactBase
	{
		[Key, Column("ContactId")]
		public Guid ContactId { get; set; }

		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string EMailAddress1 { get; set; }
		public string EMailAddress2 { get; set; }
		public string EMailAddress3 { get; set; }
		public string MobilePhone { get; set; }
		public string Telephone1 { get; set; }
		public string Telephone2 { get; set; }
		public string Telephone3 { get; set; }
		public string Fax { get; set; }
		//public ICollection<CustomerAddressBase> Addresses { get; set; }
	}
}