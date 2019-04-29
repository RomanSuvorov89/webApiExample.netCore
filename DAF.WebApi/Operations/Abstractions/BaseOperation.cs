using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DAF.DataAccess.Infrastructure;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Responses;

namespace DAF.WebApi.Operations.Abstractions
{
    public abstract class BaseOperation
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly TimeSpan TimeOfLifeToken = TimeSpan.FromMinutes(100);

        protected BaseOperation(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public abstract Response Execute();

        protected Response CreateSuccessResponse<T>(string message, T resultOperation = default(T))
        {
            return resultOperation.Equals(default(T)) ?
                new Response {IsSuccess = true, Message = message } 
                : new Response<T> {IsSuccess = true, Message = message, ResultOperation = resultOperation};
        }

        protected Response CreateSuccessResponse(string message)
        {
            return new Response {IsSuccess = true, Message = message};
        }

        protected Response CreateErrorResponse(string message)
        {
            return new Response {IsSuccess = false, Message = message};
        }

        protected string CreateAccessToken(User user)
        {
            var claimIdentity = new ClaimsIdentity(
                new List<Claim> {new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)},
                "Token",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return Helper.GenerateToken(claimIdentity.Claims, TimeOfLifeToken);
        }

        protected void SaveToDb()
        {
            UnitOfWork.Save();
        }
    }
}