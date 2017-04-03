using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;

namespace Backend.Authentication.Config
{
	public class AppOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		private readonly UserManager<IdentityUser> _user;

		public AppOAuthAuthorizationServerProvider(UserManager<IdentityUser> user)
		{
			_user = user;
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

			//Get User Information
			var user = _user.FindByName(context.UserName);
			if (user == null)
			{
				context.SetError("invalid_grant", "The user name or password is incorrect.");
				return Task.FromResult<object>(null);
			}

			//Get Roles for User
			var roles = _user.GetRoles(user.Id);
			if (roles == null)
			{
				context.SetError("invalid_grant", "Could not determine Roles for the Specified User");
				return Task.FromResult<object>(null);
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim("UserID", user.Id));
			identity.AddClaim(new Claim("UserName", user.UserName));

			foreach (var role in roles)
				identity.AddClaim(new Claim(ClaimTypes.Role, role));

			context.Validated(identity);

			return Task.FromResult<object>(null);
		}
	}
}