using System.Security.Claims;
using DAF.DataAccess.ContextFactory;
using DAF.DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace DAF.WebApi.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IUnitOfWork UnitOfWork;

        public ApiControllerBase(IContextFactory contextFactory)
        {
            UnitOfWork = contextFactory.GetContext();
        }

        protected string GetUserNameByIdentity()
        {
            return User.FindFirstValue(ClaimTypes.Name);
        }
    }
}