using CoreApi.DTO;
using CoreApi.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreApi.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private static readonly string MSG_ERRO = "Ocorreu um erro inesperado!";
        
        public ExceptionFilter() { }

        public void OnException(ExceptionContext context)
        {
            JsonResult result;
            if (context.Exception is ApiException)
            {
                var negocioException = context.Exception as ApiException;
                result = new JsonResult(new DtoError { Message = negocioException.Message, ErrorList = negocioException.Errors }) { StatusCode = (int)negocioException.StatusCode };
            }
            else
            {
                result = new JsonResult(new DtoError { Message = MSG_ERRO}) { StatusCode = StatusCodes.Status500InternalServerError };
            }
            context.Result = result;
        }
    }
}
