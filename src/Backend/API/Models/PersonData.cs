﻿using System;

namespace Backend.API.Models
{
	public class PersonData
	{
		/// <summary>
		///     Identifier for the person
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Salutation
		/// </summary>
		public string Salutation { get; set; }

		/// <summary>
		///     The person's full name
		/// </summary>
		public string FullName { get; set; }

		/// <summary>
		///     The person's first name
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		///     The person's middle name
		/// </summary>
		public string MiddleName { get; set; }

		/// <summary>
		///     The person's last name
		/// </summary>
		public string LastName { get; set; }

		/// <summary>
		///     The suffix of the person's name
		/// </summary>
		public string Suffix { get; set; }

		/// <summary>
		///     The person's work email address
		/// </summary>
		public string EMailAddressWork { get; set; }

		/// <summary>
		///     The person's home email address
		/// </summary>
		public string EMailAddressHome { get; set; }

		/// <summary>
		///     The person's mobile phone number
		/// </summary>
		public string MobilePhone { get; set; }

		/// <summary>
		///     The person's home phone number
		/// </summary>
		public string TelephoneHome { get; set; }

		/// <summary>
		///     The person's work phone number
		/// </summary>
		public string TelephoneWork { get; set; }

		/// <summary>
		///     The person's fax number
		/// </summary>
		public string Fax { get; set; }
	}
}