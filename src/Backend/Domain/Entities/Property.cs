using System;
using System.Security.AccessControl;

namespace Backend.Domain.Entities
{
	public class Property
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string AddressStreet1 { get; set; }
		public string AddressStreet2 { get; set; }
		public string AddressCity { get; set; }
		public string AddressState { get; set; }
		public string AddressZip { get; set; }
		public Guid CrmAccountId { get; set; }
	}
}