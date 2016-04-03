using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_glitemExtensionBase")]
	public class GlItemBase
	{
		[Key]
		[Column("New_glitemId")]
		public Guid GlItemId { get; set; }

		public string New_itemname { get; set; }
		public int New_Subitemof { get; set; }
	}
}