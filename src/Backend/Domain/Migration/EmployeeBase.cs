using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_employeeExtensionBase")]
	public class EmployeeBase
	{
		[Key]
		[Column("New_employeeId")]
		public Guid EmployeeId { get; set; }

		public string New_employeename { get; set; }
		public string New_MobilePhone { get; set; }
		public DateTime? New_DateHired { get; set; }
		public DateTime? New_DateTerminated { get; set; }
	}
}