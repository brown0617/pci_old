using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class County
	{
		/// <summary>
		///     Key for county record
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Name of the county
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Current sales tax rate for the county
		/// </summary>
		public decimal SalesTaxRate { get; set; }

		/// <summary>
		///     State that the county is located in
		/// </summary>
		public string StateAbbreviation { get; set; }

		[ForeignKey("StateAbbreviation")]
		public virtual State State { get; set; }

		#region remove after migration

		public Guid CrmCountyId { get; set; }

		#endregion
	}
}