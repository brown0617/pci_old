﻿using System;
using System.Collections.Generic;

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
		
		#region TODO: remove once in production

		/// <summary>
		///     Id representing the property from CRM used for migration purposes
		/// </summary>
		public Guid CrmAccountId { get; set; }

		/// <summary>
		///     Id representing the customer from CRM used for migration purposes
		/// </summary>
		public Guid CrmParentAccountId { get; set; }

		/// <summary>
		///     Id representing the primary contact from CRM used for migration purposes
		/// </summary>
		public Guid? CrmPrimaryContactId { get; set; }

		#endregion
	}
}