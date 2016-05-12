using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain.Entities
{
	public class WorkOrderTime
	{
		/// <summary>
		///     Identifier for the work order time row
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///     Identifier for the work order row
		/// </summary>
		public int WorkOrderId { get; set; }

		[ForeignKey("WorkOrderId")]
		public virtual WorkOrder WorkOrder { get; set; }

		/// <summary>
		///     Size of the crew
		/// </summary>
		public int CrewSize { get; set; }

		/// <summary>
		///     The date/time that the crew left the job site
		/// </summary>
		public DateTime? Departure { get; set; }

		/// <summary>
		///     The date/time that the crew arrived at the job site
		/// </summary>
		public DateTime? Arrival { get; set; }

		/// <summary>
		///     The date/time that the crew started their first break
		/// </summary>
		public DateTime? Break1Start { get; set; }

		/// <summary>
		///     The date/time that the crew finished their first break
		/// </summary>
		public DateTime? Break1Finish { get; set; }

		/// <summary>
		///     The date/time that the crew started their second break
		/// </summary>
		public DateTime? Break2Start { get; set; }

		/// <summary>
		///     The date/time that the crew finished their second break
		/// </summary>
		public DateTime? Break2Finish { get; set; }

		/// <summary>
		///     The date/time that the crew started their lunch break
		/// </summary>
		public DateTime? LunchStart { get; set; }

		/// <summary>
		///     The date/time that the crew finished their lunch break
		/// </summary>
		public DateTime? LunchFinish { get; set; }

		/// <summary>
		///     The foreign key to the employee entity for the foreman
		/// </summary>
		public int? ForemanId { get; set; }

		[ForeignKey("ForemanId")]
		public virtual Employee Foreman { get; set; }

		/// <summary>
		///     The calculated actual hours
		/// </summary>
		public decimal? ActualManHours { get; set; }

		/// <summary>
		///     Indicates if the job is complete
		/// </summary>
		public bool JobComplete { get; set; }
	}
}