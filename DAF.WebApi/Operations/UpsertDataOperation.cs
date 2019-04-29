using System;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Requests;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class UpsertDataOperation : AuthorizeBaseOperation
    {
        private readonly DataTokenRequest _dataTokenRequest;

        public UpsertDataOperation(IUnitOfWork unitOfWork, string identityUserName, DataTokenRequest dataTokenRequest) : base(unitOfWork, identityUserName)
        {
            _dataTokenRequest = dataTokenRequest;
        }

        protected override Response LogicImplementation()
        {
            var repository = UnitOfWork.Repository<Data>();
            string message;

            if (_dataTokenRequest.Id != Guid.Empty)
            {
                var dataFromDb = repository.GetItem(x => x.Id == _dataTokenRequest.Id);

                if (dataFromDb == null)
                {
                    return CreateErrorResponse("Ошибка продления токена - не найдены данные");
                }

                dataFromDb.Description = _dataTokenRequest.Description;
                dataFromDb.Value = _dataTokenRequest.Value;

                repository.Update(dataFromDb);
                message = "Данные успешно обновлены";
            }
            else
            {
                var newData = new Data
                {
                    Description = _dataTokenRequest.Description,
                    Value = _dataTokenRequest.Value,
                    User = User,
                    DataAccessToken = new DataAccessToken
                    {
                        ExpiresAt = DateTime.Now.Add(TimeOfLifeToken),
                        TokenString = CreateAccessToken(User)
                    }
                };

                repository.Insert(newData);
                message = "Данные успешно созданы и сохранены на сервере";
            }

            SaveToDb();
            return CreateSuccessResponse(message);
        }
    }
}