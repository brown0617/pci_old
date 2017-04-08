using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Identity_Models
{
	public class User : IdentityUser<int, UserLogin, UserRole, UserClaim>
	{
		/// <summary>
		/// Indicates inactive user
		/// </summary>
		public bool InActive { get; set; }
	}
}