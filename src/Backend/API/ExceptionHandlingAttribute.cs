using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using NLog;

namespace Backend.API
{

	public class ExceptionHandlingAttribute : ExceptionFilterAttribute
	{
		private static readonly Logger Log = LogManager.GetCurrentClassLogger();

		public override void OnException(HttpActionExecutedContext context)
		{
			var ex = context.Exception;
			Log.Error(ex);

			throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError)
			{
				Content = new StringContent(context.Exception.Message),
				ReasonPhrase = "Exception"
			});
		}
	}
}