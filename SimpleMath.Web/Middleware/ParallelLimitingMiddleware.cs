using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SimpleMath.Web.Utils;

namespace SimpleMath.Web.Middleware
{
    public class ParallelLimitingMiddleware : IMiddleware
    {
        private readonly IParallelRequestsCounter counter;

        public ParallelLimitingMiddleware(IParallelRequestsCounter counter)
        {
            this.counter = counter;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (counter.TryIncrement())
            {
                await next.Invoke(context);
                counter.Decrement();
            }
            else
            {
                context.Response.StatusCode = 503;
                await context.Response.CompleteAsync();
            }
        }
    }
}