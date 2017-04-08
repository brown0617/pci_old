using System.Threading.Tasks;
using Backend.Authentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly AuthContext _authContext;
		private readonly UserManager<IdentityUser> _userManager;

		public AuthRepository(AuthContext authContext, UserManager<IdentityUser> userManager)
		{
			_authContext = authContext;
			_userManager = userManager;
		}

		public async Task<IdentityResult> RegisterUser(AppUser appUser)
		{
			var user = new IdentityUser
			{
				UserName = appUser.UserName
			};

			var result = await _userManager.CreateAsync(user, appUser.Password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password)
		{
			var user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			_authContext.Dispose();
			_userManager.Dispose();
		}
	}
}