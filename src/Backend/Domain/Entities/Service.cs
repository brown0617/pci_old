using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class Service
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IncludeLabor { get; set; }
		public decimal LaborPrice { get; set; }
		public decimal LaborCost { get; set; }
		public bool IncludeMaterial { get; set; }
		public decimal MaterialPrice { get; set; }
		public decimal MaterialCost { get; set; }
		public int? GlItemId { get; set; }

		[ForeignKey("GlItemId")]
		public virtual GlItem GlItem { get; set; }
	}
}