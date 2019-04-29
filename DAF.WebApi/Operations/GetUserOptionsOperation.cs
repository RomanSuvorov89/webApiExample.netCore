using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class GetUserOptionsOperation : AuthorizeBaseOperation
    {
        public GetUserOptionsOperation(IUnitOfWork unitOfWork, string identityUserName) : base(unitOfWork, identityUserName) { }

        protected override Response LogicImplementation()
        {
            return CreateSuccessResponse($"Настройки пользователя {User.MiddleName} {User.FirstName[0]}. {User.LastName[0]}. успешно загружены",
                new UserData
                {
                    Email = User.Email,
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Login = User.Login,
                    Password = User.Password,
                    MiddleName = User.MiddleName
                });
        }
    }
}