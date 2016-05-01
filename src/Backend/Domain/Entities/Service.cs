using System;
using Backend.Domain.Enums;

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
		public bool CompleteCare { get; set; }
		public Season Season { get; set; }

		#region remove after migration

		public Guid CrmProductId { get; set; }

		#endregion
	}
}