using System.ComponentModel.DataAnnotations;

namespace Backend.Domain.Entities
{
	public class State
	{
		[Key]
		public string StateAbbreviation { get; set; }

		public string Name { get; set; }
	}
}