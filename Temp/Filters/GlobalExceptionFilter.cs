using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Temp.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult()
            {
                StatusCode = 200,
                ContentType = "application/json;charset=UTF-8",
                Content = JsonConvert.SerializeObject(new
                {
                    errorCode = 500,
                    isSucceed = false,
                    message = context.Exception.Message.ToString(),
                })
            };
            context.ExceptionHandled = true;
        }

    }
}
