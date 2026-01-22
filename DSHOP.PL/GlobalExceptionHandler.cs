using DSHOP.DAL.DTO.Response;
using Microsoft.AspNetCore.Diagnostics;

namespace DSHOP.PL
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception ex, CancellationToken cancellationToken)
        {
            var error = new ErrorDetails()
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Server Error!!!!!!!!!",
                StackTrace = ex.InnerException.Message
            };
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsJsonAsync(error);

            return true;
        }
    }
}
