using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DAF.DataAccess.Models;
using DAF.DataAccess.UnitOfWork;
using DAF.WebApi.Models;
using DAF.WebApi.Models.Responses;
using DAF.WebApi.Operations.Abstractions;

namespace DAF.WebApi.Operations
{
    public class UpdateUserOptionsOperation : AuthorizeBaseOperation
    {
        private readonly UserData _userRequest;

        public UpdateUserOptionsOperation(IUnitOfWork unitOfWork, string identityUserName, UserData userRequest) : base(unitOfWork, identityUserName)
        {
            _userRequest = userRequest;
        }

        protected override Response LogicImplementation()
        {
            var requestProperties = _userRequest.GetType().GetProperties();
            var updatedFields = new List<string>();

            foreach (var property in requestProperties)
            {
                if (UpdateNewData(property))
                {
                    updatedFields.Add(property.Name);
                };
            }

            if (updatedFields.Any())
            {
                var repository = UnitOfWork.Repository<User>();

                repository.UpdateByFields(User, updatedFields);
                SaveToDb();
                return CreateSuccessResponse("Данные успешно обновлены по текущему токену");
            }

            return CreateErrorResponse("Не обнаружено изменений");
        }

        private bool UpdateNewData(PropertyInfo propertyInfo)
        {
            var dbUserProperty = User.GetType().GetProperty(propertyInfo.Name);
            var requestUserProperty = _userRequest.GetType().GetProperty(propertyInfo.Name);

            if (dbUserProperty == null || requestUserProperty == null)
            {
                return false;
            }

            var requestUserValue = requestUserProperty.GetValue(_userRequest);

            if (dbUserProperty.GetValue(User).Equals(requestUserValue))
            {
                return false;
            }

            dbUserProperty.SetValue(User, requestUserValue);
            return true;
        }
    }
}