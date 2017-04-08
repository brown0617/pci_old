using System.Security.Claims;
using System.Threading.Tasks;
using Backend.Authentication.Identity_Models;
using Backend.Authentication.Repositories;
using Microsoft.Owin.Security.OAuth;

namespace Backend.Authentication.Config
{
	public class AppOAuthAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		private readonly IAuthRepository _authRepository;

		public AppOAuthAuthorizationServerProvider(IAuthRepository authRepository)
		{
			_authRepository = authRepository;
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		//public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		//{
		//	context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] {"*"});

		//	using (_authRepository)
		//	{
		//		var user = await _authRepository.FindUser(context.UserName, context.Password);

		//		if (user == null)
		//		{
		//			context.SetError("invalid_grant", "The user name or password is incorrect.");
		//			return;
		//		}
		//	}

		//	var identity = new ClaimsIdentity(context.Options.AuthenticationType);
		//	identity.AddClaim(new UserClaim("sub", context.UserName));
		//	identity.AddClaim(new Claim("role", "user"));

		//	context.Validated(identity);
		//}
	}
}