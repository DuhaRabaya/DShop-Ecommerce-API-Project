using DSHOP.DAL.DTO.Response;

namespace DSHOP.PL.Middlewares
{
    public class GlobalExceptionHandling
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandling(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex) 
            {
                var error = new ErrorDetails()
                {
                    StatusCode=StatusCodes.Status500InternalServerError,
                    Message="Server Error!!!!!!!!!",
                    StackTrace=ex.InnerException.Message
                };
                context.Response.StatusCode = error.StatusCode;
                await context.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
