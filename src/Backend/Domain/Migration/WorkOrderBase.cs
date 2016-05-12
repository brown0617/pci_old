using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Migration
{
	[Table("New_workorderExtensionBase")]
	public class WorkOrderBase
	{
		[Key]
		[Column("New_workorderId")]
		public Guid WorkOrderId { get; set; }

		public string New_name { get; set; }
		public string New_Details { get; set; }
		public DateTime? New_ScheduledStart { get; set; }
		public DateTime? New_ScheduledCompletion { get; set; }
		public int? New_ScheduledCrewSize { get; set; }
		public DateTime? New_ActualStart { get; set; }
		public DateTime? New_ActualCompletion { get; set; }
		public decimal? New_ActualManHours { get; set; }
		public int? New_ActualCrewSize { get; set; }
		public Guid? New_SalesOrder_WorkOrderId { get; set; }
		public string New_VarianceExplanation { get; set; }
		public float? New_ManHourVariance { get; set; }
		public string New_InvoiceNumber { get; set; }
		public string New_BillingComments { get; set; }
		public int? New_VisitNumber { get; set; }
		public int? New_Visits { get; set; }
		public string New_BillingMethodDescription { get; set; }
		public DateTime? New_ServiceDeadline { get; set; }
		public string New_ServiceFrequency { get; set; }
		public string New_CustomerName { get; set; }
		public int? New_WeekDay { get; set; }
		public DateTime? New_ProjectedStart { get; set; }
		public DateTime? New_ProjectedCompletion { get; set; }
		public Guid? New_ForemanId { get; set; }
	}
}