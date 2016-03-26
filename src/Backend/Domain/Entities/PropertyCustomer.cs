using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class PropertyCustomer
	{
		/// <summary>
		///     Unique Identifier for each property/customer relationship
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Foreign key for property
		/// </summary>
		public int PropertyId { get; set; }

		[ForeignKey("PropertyId")]
		public virtual Property Property { get; set; }

		/// <summary>
		///     Foreign key for customer
		/// </summary>
		public int CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		public int? PrimaryContactId { get; set; }

		[ForeignKey("PrimaryContactId")]
		public virtual Person Person { get; set; }
	}
}