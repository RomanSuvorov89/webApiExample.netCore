using DAF.DataAccess.ContextFactory;
using DAF.WebApi.Models;
using DAF.WebApi.Models.Requests;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DAF.WebApi.Controllers
{
    [Authorize]
    public class MainController : ApiControllerBase
    {
        public MainController(IContextFactory context) : base(context) { }

        [HttpPost("getUserTokens")]
        public Response GetUserTokens()
        {
            return new GetUserTokensOperation(UnitOfWork, GetUserNameByIdentity()).Execute();
        }

        [HttpPost("deleteTokensByUser")]
        public Response DeleteTokenByUser()
        {
            return new DeleteTokenUserOperation(UnitOfWork, GetUserNameByIdentity()).Execute();
        }

        [HttpPost("getUserOptions")]
        public Response GetUserOptions()
        {
            return new GetUserOptionsOperation(UnitOfWork, GetUserNameByIdentity()).Execute();
        }

        [HttpPost("updateUserOptions")]
        public Response UpdateUserOptions([FromBody]UserData userRequest)
        {
            return new UpdateUserOptionsOperation(UnitOfWork, GetUserNameByIdentity(), userRequest).Execute();
        }

        [HttpPost("getUserData")]
        public Response GetUserData()
        {
            return new GetUserDataOperation(UnitOfWork, GetUserNameByIdentity()).Execute();
        }

        [HttpPost("upsertData")]
        public Response UpsertData([FromBody]DataTokenRequest dataTokenRequest)
        {
            return new UpsertDataOperation(UnitOfWork, GetUserNameByIdentity(), dataTokenRequest).Execute();
        }

        [HttpPost("updateTokenForData")]
        public Response UpdateTokenForData([FromBody]DataTokenRequest dataTokenRequest)
        {
            return new UpdateTokenForDataOperation(UnitOfWork, GetUserNameByIdentity(), dataTokenRequest).Execute();
        }
    }
}