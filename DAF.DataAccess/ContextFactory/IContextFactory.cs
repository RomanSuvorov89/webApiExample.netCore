using DAF.DataAccess.UnitOfWork;

namespace DAF.DataAccess.ContextFactory
{
    public interface IContextFactory
    {
        IUnitOfWork GetContext();
    }
}