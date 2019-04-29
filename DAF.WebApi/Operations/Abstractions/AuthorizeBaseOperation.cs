using System.Linq;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Responses;

namespace DAF.WebApi.Operations.Abstractions
{
    public abstract class AuthorizeBaseOperation : BaseOperation
    {
        protected readonly User User;

        protected AuthorizeBaseOperation(IUnitOfWork unitOfWork, string identityUserName) : base(unitOfWork)
        {
            User = GetUserByName(identityUserName);
        }

        protected abstract Response LogicImplementation();

        public override Response Execute()
        {
            return User != null ? LogicImplementation() : CreateErrorResponse("Доступ закрыт");
        }

        private User GetUserByName(string userName)
        {
            return UnitOfWork.Repository<User>().GetItems(x => x.Login == userName).FirstOrDefault();
        }
    }
}