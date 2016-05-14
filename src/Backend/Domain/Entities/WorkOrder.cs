using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class WorkOrder
	{
		public int Id { get; set; }
		public int OrderItemId { get; set; }

		[ForeignKey("OrderItemId")]
		public virtual OrderItem OrderItem { get; set; }

		public string Details { get; set; }
		public DateTime? ScheduledStart { get; set; }
		public DateTime? ScheduledCompletion { get; set; }
		public int? ScheduledCrewSize { get; set; }
		public DateTime? ActualStart { get; set; }
		public DateTime? ActualCompletion { get; set; }
		public decimal? ActualManHours { get; set; }
		public int? ActualCrewSize { get; set; }
		public string VarianceExplanation { get; set; }
		public float? ManHourVariance { get; set; }
		public string InvoiceNumber { get; set; }
		public string BillingComments { get; set; }
		public int? VisitNumber { get; set; }
		public DateTime? ProjectedStart { get; set; }
		public DateTime? ProjectedCompletion { get; set; }
		public int? ForemanId { get; set; }

		[ForeignKey("ForemanId")]
		public virtual Employee Foreman { get; set; }

		#region Remove after migration

		public Guid CrmWorkOrderId { get; set; }

		#endregion
	}
}