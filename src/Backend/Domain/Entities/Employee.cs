using System;

namespace Backend.Domain.Entities
{
	public class Employee
	{
		/// <summary>
		///     Key for employee record
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Employee name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///     Employee's mobile phone number
		/// </summary>
		public string MobilePhone { get; set; }

		/// <summary>
		///     The date the employee was hired
		/// </summary>
		public DateTime? DateHired { get; set; }

		/// <summary>
		///     The date the employee was terminated
		/// </summary>
		public DateTime? DateTerminated { get; set; }

		#region remove after migration

		public Guid CrmEmployeeId { get; set; }

		#endregion
	}
}