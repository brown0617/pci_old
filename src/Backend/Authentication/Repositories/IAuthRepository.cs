using System;
using System.Threading.Tasks;
using Backend.Authentication.Identity_Models;
using Backend.Authentication.Models;
using Microsoft.AspNet.Identity;

namespace Backend.Authentication.Repositories
{
	public interface IAuthRepository : IDisposable
	{
		Task<IdentityResult> RegisterUser(AppUser appUser);
		Task<User> FindUser(string userName, string password);
	}
}