using System;
using System.Threading.Tasks;
using DAF.DataAccess.ContextFactory;
using DAF.DataAccess.Models;
using DAF.WebApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DAF.WebApi.Filters
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var exceptionStack = context.Exception.StackTrace;
            var exceptionMessage = context.Exception.Message;
            var message = $"При выполнении {actionName} возникло исключение: \n {exceptionMessage}";

            context.Result = new ObjectResult(new Response
            {
                IsSuccess = false,
                Message = message
            });

            var unitOfWork = context.HttpContext.RequestServices.GetRequiredService<IContextFactory>().GetContext();

            Task.Run(() =>
            {
                unitOfWork.Repository<LogEntry>().Insert(new LogEntry
                {
                    Title = "Произошла ошибка!",
                    Message = $"{message} \n {exceptionStack}",
                    OperationName = actionName
                });

                unitOfWork.Save();
            });

            context.ExceptionHandled = true;
        }
    }
}