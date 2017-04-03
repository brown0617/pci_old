using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Models
{
	public class AppUser : IdentityUser
	{
		public int UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[Required]
		public bool InActive { get; set; }
	}
}