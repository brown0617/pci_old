using System.Threading.Tasks;
using System.Web.Http;
using Backend.Authentication.Models;
using Backend.Authentication.Repositories;
using Microsoft.AspNet.Identity;

namespace Backend.Authentication.Controllers
{
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		private readonly AuthRepository _authRepository;

		public AccountController(AuthRepository authRepository)
		{
			_authRepository = authRepository;
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<IHttpActionResult> Register(AppUser appUser)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var result = await _authRepository.RegisterUser(appUser);

			var errorResult = GetErrorResult(result);

			if (errorResult != null)
				return errorResult;

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_authRepository.Dispose();

			base.Dispose(disposing);
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
				return InternalServerError();

			if (!result.Succeeded)
			{
				if (result.Errors != null)
					foreach (var error in result.Errors)
						ModelState.AddModelError("", error);

				if (ModelState.IsValid)
					return BadRequest();

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}