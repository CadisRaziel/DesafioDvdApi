using Microsoft.AspNetCore.Diagnostics;
using FluentValidation;
using Microsoft.Data.SqlClient;
using DesafioDvD.Core.DomainObjects;


namespace DesafioDvD.WebApi.Setup
{
    //Antes do .net 8 tinhamos o middleware onde toda request rodava dentro de um middleware e a gente tinha um tratamento de erro generico ou especifico
    //No .net8 temos o IExceptionHandler que vai realizar o tratamento em caso de exception
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            this.logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                ArgumentNullException argumentException => (500, argumentException.Message),
                DomainException domainException => (500, domainException.Message),
                SqlException sqlException => (500, sqlException.Message),
                ValidationException validationException => (500, validationException.Message),
                _ => (500, "Something went wrong")
            };

            logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(errorMessage, cancellationToken);
            return true;
        }
    }
}
