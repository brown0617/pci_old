using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	public class WorkOrderBase
	{
		[Key]
		[Column("New_workorderId")]
		public Guid WorkOrderId { get; set; }
		public DateTime? CreatedOn { get; set; }
		public DateTime? ModifiedOn { get; set; }
	}
}