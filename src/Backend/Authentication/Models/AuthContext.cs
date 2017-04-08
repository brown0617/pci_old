using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Models
{
	public class AuthContext : IdentityDbContext<IdentityUser>
	{
		public AuthContext()
			: base("Auth")
		{
		}

		public IDbSet<AppUser> AppUsers { get; set; }
	}

	public class AuthDbInitializer : DropCreateDatabaseIfModelChanges<AuthContext>
	{
		protected override void Seed(AuthContext authContext)
		{
			base.Seed(authContext);
		}
	}
}