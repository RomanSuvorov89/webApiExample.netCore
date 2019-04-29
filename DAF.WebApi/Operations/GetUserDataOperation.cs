using System;
using System.Linq;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class GetUserDataOperation : AuthorizeBaseOperation
    {
        public GetUserDataOperation(IUnitOfWork unitOfWork, string identityUserName) : base(unitOfWork, identityUserName) { }

        protected override Response LogicImplementation()
        {
            var datas = UnitOfWork.Repository<Data>().GetItems(x => x.User == User);

            var response = new UserDataResponse
            {
                ActiveTokenData = datas.Where(x => x.DataAccessToken.ExpiresAt > DateTime.Now).Select(x => new DataToken { Id = x.Id, Description = x.Description, Value = x.Value, Token = x.DataAccessToken.TokenString }).ToList(),
                ExpiredTokenData = datas.Where(x => x.DataAccessToken.ExpiresAt < DateTime.Now).Select(x => new DataToken { Id = x.Id, Description = x.Description, Value = "Данные скрыты", Token = x.DataAccessToken.TokenString }).ToList()
            };

            return CreateSuccessResponse("Данные успешно загружены. " +
                                         $"Активные данные с валидным токеном: {response.ActiveTokenData.Count} шт. " +
                                         $"Данных с окончившимся сроком действия токенов: {response.ExpiredTokenData.Count} шт.", response);
        }
    }
}