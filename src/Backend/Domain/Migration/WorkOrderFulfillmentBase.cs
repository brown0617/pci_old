using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_workorderfulfillmentExtensionBase")]
	public class WorkOrderFulfillmentBase
	{
		[Key]
		[Column("New_workorderfulfillmentId")]
		public Guid WorkOrderFulfillmentId { get; set; }

		[Column("New_workorderId")]
		public Guid? WorkOrderId { get; set; }

		public int? New_CrewSize { get; set; }
		public DateTime? New_Departure { get; set; }
		public DateTime? New_Arrival { get; set; }
		public DateTime? New_Break1Start { get; set; }
		public DateTime? New_Break1Finish { get; set; }
		public DateTime? New_Break2Start { get; set; }
		public DateTime? New_Break2Finish { get; set; }
		public DateTime? New_LunchStart { get; set; }
		public DateTime? New_LunchFinish { get; set; }
		public Guid? New_CrewForemanId { get; set; }
		public decimal? New_ActualManHours { get; set; }
		public bool? New_JobComplete { get; set; }
	}
}