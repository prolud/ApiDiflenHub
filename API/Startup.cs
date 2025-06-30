using Application.UseCases;
using Domain.Interfaces;
using Infra;
using Infra.Services;
using Scalar.AspNetCore;

namespace API
{
    public static class Startup
    {
        public static void SetImplementations(IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UsersUseCase>();
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddControllers();
        }

        public static void ConfigureScalar(WebApplication app)
        {
            app.UseSwagger();
            app.MapScalarApiReference(op =>
            {
                op.AddDocument("Doc", routePattern: "/swagger/v1/swagger.json");
            });
        }

        public static void ConfigureAPI(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}