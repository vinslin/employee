using System.Net;
using System.Text.Json;

namespace EmployeeAdminPortal.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Go to the next middleware/controller
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
private static Task HandleExceptionAsync(HttpContext context, Exception exception)
{
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    // Get the root/original error
    var realException = GetInnermostException(exception);

    var response = new
    {
        StatusCode = context.Response.StatusCode,
        Message = realException.Message // Only show the actual root error
    };
    
    var json = JsonSerializer.Serialize(response);
    return context.Response.WriteAsync(json);
}

private static Exception GetInnermostException(Exception ex)
{
    while (ex.InnerException != null)
        ex = ex.InnerException;
    return ex;
}

    }
}
