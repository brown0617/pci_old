using Backend.Domain;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Identity_Models
{
	public class UserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
	{
		public UserStore(AppDbContext context)
			: base(context)
		{
		}
	}
}