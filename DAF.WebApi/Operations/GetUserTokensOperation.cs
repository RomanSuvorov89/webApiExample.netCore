using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class GetUserTokensOperation : AuthorizeBaseOperation
    {
        public GetUserTokensOperation(IUnitOfWork unitOfWork, string identityUserName) : base(unitOfWork, identityUserName) { }

        protected override Response LogicImplementation()
        {
            var userTokens = UnitOfWork.Repository<UserToken>().GetItems(x => x.User == User).ToList();

            var response = new UserTokenResponse
            {
                UserTokens = userTokens.Select(x => new UserTokenData
                {
                    DeviceName = x.DeviceName,
                    Id = x.Id,
                    LastVisit = x.LastVisit
                }).ToList()
            };

            return CreateSuccessResponse("Токены успешно загружены", response);
        }
    }
}