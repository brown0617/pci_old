using System;
using System.Threading.Tasks;
using Backend.Authentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Backend.Authentication.Repositories
{
	public interface IAuthRepository : IDisposable
	{
		Task<IdentityResult> RegisterUser(AppUser appUser);
		Task<IdentityUser> FindUser(string userName, string password);
	}
}