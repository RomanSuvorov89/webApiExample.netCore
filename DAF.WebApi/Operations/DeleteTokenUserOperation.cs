using System.Linq;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class DeleteTokenUserOperation : AuthorizeBaseOperation
    {
        public DeleteTokenUserOperation(IUnitOfWork unitOfWork, string identityUserName) : base(unitOfWork, identityUserName) { }

        protected override Response LogicImplementation()
        {
            var userTokens = UnitOfWork.Repository<UserToken>().GetItems(x => x.User == User).ToList();
            UnitOfWork.Repository<UserToken>().Delete(userTokens.Select(x => x.Id).ToArray());

            SaveToDb();

            return CreateSuccessResponse("Токены успешно удалены");
        }
    }
}