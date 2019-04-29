using DAF.DataAccess.UnitOfWork;
using DAF.Infrastructure.Models.Requests;
using DAF.Infrastructure.Models.Responses;
using DAF.Infrastructure.Operations.Abstractions;

namespace DAF.Infrastructure.Operations
{
    public class UserAuthenticationOperation : BaseOperation<LoginRequest>
    {
        public UserAuthenticationOperation(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public override Response Execute(LoginRequest request)
        {
            return default(Response);
        }
    }
}