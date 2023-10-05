using CashFlowzBackend.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CashFlowzBackend.Infrastructure.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ICustomException customException)
            {
                var response = new
                {
                    Code = customException.Code,
                    Title = customException.Title,
                    Description = customException.Description
                };

                // Return a custom response with the information
                context.HttpContext.Response.StatusCode = Convert.ToInt32(customException.Code);
                context.Result = new JsonResult(response);

                // Mark the exception as handled
                context.ExceptionHandled = true;
            }
        }
    }

}
