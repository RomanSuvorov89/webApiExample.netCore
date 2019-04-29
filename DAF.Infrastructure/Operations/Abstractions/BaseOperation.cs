using DAF.DataAccess.UnitOfWork;
using DAF.Infrastructure.Models.Responses;

namespace DAF.Infrastructure.Operations.Abstractions
{
    public abstract class BaseOperation<TRequest>
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected BaseOperation(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public abstract Response Execute(TRequest request);
    }
}