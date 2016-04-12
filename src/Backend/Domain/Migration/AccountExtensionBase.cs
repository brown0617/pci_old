using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class AccountExtensionBase
	{
		[Key]
		public Guid AccountId { get; set; }

		public string New_profile_mowing { get; set; }
		public string New_profile_snowremoval { get; set; }
		public string New_profile_bedmaintenance { get; set; }
		public string New_profile_shrubpruning { get; set; }
		public string New_profile_springcleanup { get; set; }
		public string New_profile_fallcleanup { get; set; }
		public string New_profile_MulchInstallation { get; set; }
		public Guid? New_Address1_CountyId { get; set; }

		[ForeignKey("New_Address1_CountyId")]
		public virtual CountyExtensionBase County { get; set; }

		public string New_profile_DormantTreePruning { get; set; }
		public int? New_numberofunits { get; set; }
		public int? New_PropertyType { get; set; }
		public decimal? New_MowableAreaAcres { get; set; }
		public int? New_numberofbuildings { get; set; }
	}
}