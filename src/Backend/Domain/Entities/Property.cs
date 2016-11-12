using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class Property
	{
		/// <summary>
		///     Property Id
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Name of the property
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Street address line 1 of the property
		/// </summary>
		public string AddressStreet1 { get; set; }

		/// <summary>
		///     Street address line 2 of the property
		/// </summary>
		public string AddressStreet2 { get; set; }

		/// <summary>
		///     City the property is located in
		/// </summary>
		public string AddressCity { get; set; }

		/// <summary>
		///     State the property is located in
		/// </summary>
		public string AddressState { get; set; }

		/// <summary>
		///     Zip code for the property
		/// </summary>
		public string AddressZip { get; set; }

		/// <summary>
		///     Identifier for county that the property is located in
		/// </summary>
		public int AddressCountyId { get; set; }

		/// <summary>
		/// Foreign key relationship to County entity
		/// </summary>
		[ForeignKey("AddressCountyId")]
		public virtual County County { get; set; }

		/// <summary>
		///     Foreign key for customer
		/// </summary>
		public int? CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public virtual Customer Customer { get; set; }

		/// <summary>
		///     Foreign key for primary contact
		/// </summary>
		public int? PrimaryContactId { get; set; }

		[ForeignKey("PrimaryContactId")]
		public virtual Person PrimaryContact { get; set; }

		#region TODO: remove once in production

		/// <summary>
		///     Id representing the property from CRM used for migration purposes
		/// </summary>
		public Guid CrmAccountId { get; set; }

		/// <summary>
		///     Id representing the customer from CRM used for migration purposes
		/// </summary>
		public Guid? CrmParentAccountId { get; set; }

		/// <summary>
		///     Id representing the primary contact from CRM used for migration purposes
		/// </summary>
		public Guid? CrmPrimaryContactId { get; set; }

		#endregion
	}
}