using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_countysalestaxExtensionBase")]
	public class CountySalesTaxExtensionBase
	{
		[Key]
		public Guid New_countysalestaxId { get; set; }

		[Column("New_CountyId")]
		public Guid? CountyId { get; set; }

		public decimal? New_salestaxrate { get; set; }
		public DateTime? New_StartDate { get; set; }
		public DateTime? New_EndDate { get; set; }
	}
}