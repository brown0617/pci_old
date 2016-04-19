using System;

namespace Backend.API.Models
{
	public class MaterialData
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public int? GlItemId { get; set; }
	}
}