﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class Service
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public int? GlItemId { get; set; }

		[ForeignKey("GlItemId")]
		public virtual GlItem GlItem { get; set; }

		#region remove after migration

		public Guid CrmProductId { get; set; }

		#endregion
	}
}