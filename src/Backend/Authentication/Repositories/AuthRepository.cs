using System.Threading.Tasks;
using Backend.Authentication.Identity_Models;
using Backend.Authentication.Models;
using Backend.Domain;
using Microsoft.AspNet.Identity;

namespace Backend.Authentication.Repositories
{
	public class AuthRepository : IAuthRepository
	{
		private readonly AppDbContext _ctx;
		private readonly AppUserManager _userManager;

		public AuthRepository(AppDbContext ctx, AppUserManager userManager)
		{
			_ctx = ctx;
			_userManager = userManager;
		}

		public async Task<IdentityResult> RegisterUser(AppUser appUser)
		{
			var user = new User
			{
				UserName = appUser.UserName
			};

			var result = await _userManager.CreateAsync(user, appUser.Password);

			return result;
		}

		public async Task<User> FindUser(string userName, string password)
		{
			var user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			_ctx.Dispose();
			_userManager.Dispose();
		}
	}
}