using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Migration
{
	public class ProductBase
	{
		[Key]
		public Guid ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int ProductTypeCode { get; set; }
		public decimal? Price { get; set; }
		public decimal? CurrentCost { get; set; }
	}
}