using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Backend.Authentication.Config;
using Backend.Authentication.Identity_Models;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Ninject;
using Owin;

namespace Backend
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			var config = new HttpConfiguration();

			// enable cross-origin requests
			config.EnableCors(new EnableCorsAttribute("http://localhost:9000", "*", "*"));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute("Default", "api/{controller}/{id}",
				new {id = RouteParameter.Optional, controller = "values"});

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
			jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

			var kernel = ConfigureNinject(app, config);
			
			app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

			app.UseOAuthAuthorizationServer(kernel.Get<AppOAuthAuthorizationServerOptions>().GetOptions());
			app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
		}

	}
}