using Microsoft.AspNetCore.Builder;

namespace DogsAPI.Startup
{
    public static class MiddlewareInitializer
    {
        public static WebApplication ConfigureMiddleware(this WebApplication app)
        {
            app.UseSwagger().UseSwaggerUI();

            app.UseHttpsRedirection();

            return app;
        }
    }
}
