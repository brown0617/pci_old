using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using Backend.Authentication.Config;
using Ninject;
using Owin;

namespace Backend
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			var config = new HttpConfiguration();

			// enable cross-origin requests
			config.EnableCors(new EnableCorsAttribute("http://localhost:9000", "*", "*"));

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute("Default", "api/{controller}/{id}",
				new {id = RouteParameter.Optional, controller = "values"});

			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			var kernel = ConfigureNinject(appBuilder, config);

			appBuilder.UseOAuthAuthorizationServer(kernel.Get<AppOAuthAuthorizationServerOptions>().GetOptions());
		}
	}
}