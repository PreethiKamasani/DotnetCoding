using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services.Exceptions
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> _logger;


        public ExceptionHandler(ILogger<ExceptionHandler> logger
            )
        {
            _logger = logger;

        }

        public ErrorResponse HandleException(Exception exception)
        {
            var error = exception switch
            {

                ResourceNotFoundException resourceNotFoundException => HandleResourceNotFoundException(resourceNotFoundException),
                RequestNullException requestNullException => HandleRequestNullException(requestNullException),
                UnHandledException unHandledException => HandleUnHandledException(unHandledException),
            };



            return error;
        }

        private ErrorResponse HandleResourceNotFoundException(ResourceNotFoundException resourceNotFoundException)
        {
            _logger.LogInformation(resourceNotFoundException, resourceNotFoundException.Message);

            return new ErrorResponse
            {
                Title = resourceNotFoundException.Message,
                StatusCode = HttpStatusCode.NotFound,
            };
        }
        private ErrorResponse HandleRequestNullException(RequestNullException requestNullException)
        {
            _logger.LogInformation(requestNullException, requestNullException.Message);

            return new ErrorResponse
            {
                Title = requestNullException.Message,
                StatusCode = HttpStatusCode.BadRequest,
            };
        }

        private ErrorResponse HandleUnHandledException(UnHandledException unHandledException)
        {
            _logger.LogInformation(unHandledException, unHandledException.Message);

            return new ErrorResponse
            {
                Title = unHandledException.Message,
                StatusCode = HttpStatusCode.InternalServerError,
            };
        }
    }
}
