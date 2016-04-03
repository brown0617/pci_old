using System;

namespace Backend.Domain.Entities
{
	public class GlItem
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int SubitemOf { get; set; }

		#region remove after migration

		public Guid CrmGlItemId { get; set; }

		#endregion
	}
}