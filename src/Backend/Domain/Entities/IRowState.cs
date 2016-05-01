using System;

namespace Backend.Domain.Entities
{
	public interface IRowState
	{
		/// <summary>
		///     Date the row was created
		/// </summary>
		DateTime CreatedOn { get; set; }

		/// <summary>
		///     Date the row was deleted
		/// </summary>
		DateTime? DeletedOn { get; set; }

		/// <summary>
		///     Date the row was modified last
		/// </summary>
		DateTime ModifiedOn { get; set; }
	}
}