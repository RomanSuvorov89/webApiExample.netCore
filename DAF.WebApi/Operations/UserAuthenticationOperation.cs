using System;
using System.Linq;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models.Requests;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class UserAuthenticationOperation : BaseOperation
    {
        private readonly LoginRequest _request;
        public UserAuthenticationOperation(IUnitOfWork unitOfWork, LoginRequest request) : base(unitOfWork)
        {
            _request = request;
        }
        public override Response Execute()
        {
            if (string.IsNullOrWhiteSpace(_request.Login) || string.IsNullOrWhiteSpace(_request.Password))
            {
                return CreateErrorResponse("Имя пользователя или пароль пустые");
            }

            var user = UnitOfWork.Repository<User>().GetItem(x => string.Equals(x.Login, _request.Login, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
            {
                return CreateErrorResponse("Не найден пользователь с введенными данными");
            }

            if (user.Password != _request.Password)
            {
                return CreateErrorResponse("Не верно введены данные");
            }

            var repository = UnitOfWork.Repository<UserToken>();
            var accessTokens = repository.GetItems(x => x.User == user).ToList();

            if (accessTokens.Any())
            {
                var expiredTokens = accessTokens.Where(x => x.ExpiresAt < DateTime.Now).ToList();
                repository.Delete(expiredTokens.Select(x => x.Id).ToArray());
            }

            var token = CreateAccessToken(user);
            var newAccessToken = new UserToken
            {
                User = user,
                ExpiresAt = DateTime.Now + TimeOfLifeToken,
                TokenString = token,
                DeviceName = _request.DeviceName ?? "Unknown device",
                LastVisit = DateTime.Now
            };

            repository.Insert(newAccessToken);
            SaveToDb();

            return CreateSuccessResponse($"{user.Login} успешно прошла аутентификацию", token);
        }
    }
}