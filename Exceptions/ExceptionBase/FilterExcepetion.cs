using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Exceptions.ExceptionBase;

public class FilterExcepetion : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is SystemTaskException)
        {
            ProcessMessageSistemaException(context);
        }
        else
        {
            SendMessageErroUnknown(context);
        }
    }

    private void ProcessMessageSistemaException(ExceptionContext context)
    {
        if (context.Exception is ErroValidatorException)
        {
            ProcessErroValidatorException(context);
        }
        else if(context.Exception is LoginInvalideException)
        {
            LoginErroValidatorException(context);
        }
    }

    private void ProcessErroValidatorException(ExceptionContext context)
    {
        var erroValidatorException = context.Exception as ErroValidatorException;

        if (!erroValidatorException.MesssageError.Contains(ResourceMenssagensErro.NOT_FOUND))
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        context.Result = new ObjectResult(new ResponseErroJson(erroValidatorException.MesssageError));
    }

    private void LoginErroValidatorException(ExceptionContext context)
    {
        var erroLogin = context.Exception as LoginInvalideException;
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        context.Result = new ObjectResult(new ResponseErroJson(erroLogin.Message));

    }

    private void SendMessageErroUnknown(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErroJson(ResourceMenssagensErro.ERRO_DESCONHECIDO));
    }
}

