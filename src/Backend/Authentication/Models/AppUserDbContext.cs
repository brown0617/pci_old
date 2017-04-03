using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Models
{
	public class AppUserDbContext : IdentityDbContext<AppUser>
	{
		public AppUserDbContext()
			: base("PCI")
		{
		}

		public IDbSet<AppUser> AppUsers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Configure default schema
			modelBuilder.HasDefaultSchema("User");
		}

	}
}