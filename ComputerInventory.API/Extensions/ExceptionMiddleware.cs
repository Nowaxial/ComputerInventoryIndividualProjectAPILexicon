using ComputerInventory.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ComputerInventory.API.Extensions
{
    public static class ExceptionMiddleware
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            var env = app.Services.GetRequiredService<IWebHostEnvironment>();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature is not null)
                    {
                        // Bestäm statuskod beroende på exception-typ
                        int statusCode = contextFeature.Error switch
                        {
                            KeyNotFoundException => StatusCodes.Status404NotFound,
                            InvalidOperationException => StatusCodes.Status400BadRequest,

                            UserNotFoundException => StatusCodes.Status404NotFound,
                            UserNotFoundByNameException => StatusCodes.Status404NotFound,
                            UserAlreadyExistsException => StatusCodes.Status409Conflict,
                            UserFoundWithTheSameNameException => StatusCodes.Status400BadRequest,
                            MaxUsersReachedInInventoryException => StatusCodes.Status409Conflict,
                            NoUsersFoundException => StatusCodes.Status404NotFound,

                            InventoryAlreadyExistsException => StatusCodes.Status409Conflict,
                            InventoryNotFoundException => StatusCodes.Status404NotFound,
                            NoInventoriesFoundException => StatusCodes.Status404NotFound,

                            _ => StatusCodes.Status500InternalServerError
                        };

                        context.Response.StatusCode = statusCode;

                        // Använd vår egen metod som hämtar Title från exception om möjligt
                        var problemDetails = CreateProblemDetails(contextFeature.Error, statusCode);

                        await context.Response.WriteAsJsonAsync(problemDetails);
                    }
                });
            });
        }

        private static ProblemDetails CreateProblemDetails(Exception exception, int statusCode)
        {
            return new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitle(exception),
                Detail = GetMessage(exception)
            };
        }

        private static string? GetTitle(Exception exception)
        {
            // Försök hämta Title-egenskapen via reflection
            return exception.GetType()
                .GetProperty("Title")?
                .GetValue(exception) as string ?? exception.Message;
        }

        private static string GetMessage(Exception exception)
        {
            return exception.Message;
        }
    }
}