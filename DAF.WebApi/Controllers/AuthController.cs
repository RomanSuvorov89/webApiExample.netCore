using DAF.DataAccess.ContextFactory;
using DAF.WebApi.Models.Requests;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAF.WebApi.Controllers
{
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        public AuthController(IContextFactory contextFactory) : base(contextFactory) { }

        [HttpPost("login")]
        public Response Authentication([FromBody]LoginRequest request)
        {
            return new UserAuthenticationOperation(UnitOfWork, request).Execute();
        }
    }
}