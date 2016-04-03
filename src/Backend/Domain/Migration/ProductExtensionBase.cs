using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class ProductExtensionBase
	{
		[Key]
		public Guid ProductId { get; set; }
		public Guid? New_GLItemId { get; set; }
		public string New_Description { get; set; }

		[ForeignKey("New_GLItemId")]
		public virtual GlItemBase GlItem { get; set; }
	}
}