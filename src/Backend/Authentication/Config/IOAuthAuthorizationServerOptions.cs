using Microsoft.Owin.Security.OAuth;

namespace Backend.Authentication.Config
{
	public interface IOAuthAuthorizationServerOptions
	{
		OAuthAuthorizationServerOptions GetOptions();
	}
}