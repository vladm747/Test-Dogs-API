
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsAPI.Filters
{
    public class DogExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;
            IActionResult actionResult = ex switch
            {
                KeyNotFoundException => new NotFoundObjectResult(new ErrorDTO { Message = ex.Message }),
                //ArgumentNullException => new BadRequestObjectResult(new ErrorDTO { Message = ex.Message }),
                //InvalidOperationException => new BadRequestObjectResult(new ErrorDTO { Message = ex.Message }),
                //ArgumentException => new BadRequestObjectResult(new ErrorDTO { Message = ex.Message })
                //{
                //    StatusCode = 500
                //},
                _ => new BadRequestObjectResult(new ErrorDTO() { Message = ex.Message }) { StatusCode = 500}
            };

            context.ExceptionHandled = true;
            context.Result = actionResult;
        }
    }
}
