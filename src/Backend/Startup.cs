using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

namespace Backend
{
	public class Startup
	{
		public void Configuration(IAppBuilder appBuilder)
		{
			var config = new HttpConfiguration();

			// enable cross-origin requests
			config.EnableCors(new EnableCorsAttribute("http://localhost:9000", "*", "*"));

			// Web API routes
			config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}",
				new {id = RouteParameter.Optional, controller = "values"});
			
			config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

			appBuilder.UseNinjectMiddleware(NinjectConfig.CreateKernel);

			appBuilder.UseNinjectWebApi(config); 
		}
	}
}