using System;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Requests;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class UpdateTokenForDataOperation : AuthorizeBaseOperation
    {
        private readonly DataTokenRequest _dataTokenRequest;

        public UpdateTokenForDataOperation(IUnitOfWork unitOfWork, string identityUserName, DataTokenRequest dataTokenRequest) : base(unitOfWork, identityUserName)
        {
            _dataTokenRequest = dataTokenRequest;
        }

        protected override Response LogicImplementation()
        {
            var dataFromDb = UnitOfWork.Repository<Data>().GetItem(x => x.Id == _dataTokenRequest.Id);

            if (dataFromDb == null)
            {
                return CreateErrorResponse("Ошибка продления токена - не найдены данные");
            }

            var token = CreateAccessToken(User);

            var repository = UnitOfWork.Repository<DataAccessToken>();
            var dataAccessToken = repository.GetItem(x => x.Data == dataFromDb);

            if (dataAccessToken == null)
            {
                return CreateErrorResponse("Не найден истекший токен");
            }

            dataAccessToken.TokenString = token;
            dataAccessToken.ExpiresAt = DateTime.Now.Add(TimeOfLifeToken);

            repository.Update(dataAccessToken);
            SaveToDb();

            return CreateSuccessResponse("Токен данных был успешно обновлён");
        }
    }
}