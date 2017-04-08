using Backend.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Backend.Authentication.Identity_Models
{
	public class AppUserManager : UserManager<User, int>
	{
		public AppUserManager(IUserStore<User, int> store)
			: base(store)
		{
		}
		public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
		{
			var manager = new AppUserManager(new UserStore(context.Get<AppDbContext>()));

			manager.UserValidator = new UserValidator<User, int>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 5,
				RequireNonLetterOrDigit = false,     // true
				// RequireDigit = true,
				RequireLowercase = false,
				RequireUppercase = false,
			};

			return (manager);
		}
	}
}