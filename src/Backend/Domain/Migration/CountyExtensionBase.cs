using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_countyExtensionBase")]
	public class CountyExtensionBase
	{
		[Key]
		[Column("New_countyId")]
		public Guid CountyId { get; set; }

		public string New_countyname { get; set; }
	}
}