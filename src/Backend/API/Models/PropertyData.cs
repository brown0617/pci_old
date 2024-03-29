﻿namespace Backend.API.Models
{
	public class PropertyData
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
		///     Street Address for the property
		/// </summary>
		public string StreetAddress
		{
			get
			{
				var streetAddress = string.IsNullOrEmpty(AddressStreet2)
					? AddressStreet1
					: string.Concat(AddressStreet1, ",", AddressStreet2);
				return streetAddress;
			}
		}

		/// <summary>
		///     Foreign key for customer
		/// </summary>
		public int CustomerId { get; set; }

		/// <summary>
		///     Name of the customer
		/// </summary>
		public string CustomerName { get; set; }

		/// <summary>
		///     Foreign key for primary contact
		/// </summary>
		public int? PrimaryContactId { get; set; }

		/// <summary>
		///     Name of the primary contact
		/// </summary>
		public string PrimaryContactName { get; set; }

		/// <summary>
		///     Name of the primary contact
		/// </summary>
		public string PrimaryContactPhone { get; set; }

		/// <summary>
		///     Email address of the primary contact
		/// </summary>
		public string PrimaryContactEMail { get; set; }
	}
}