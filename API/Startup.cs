using Application.UseCases;
using Domain.Interfaces;
using Infra;
using Infra.Services;
using Scalar.AspNetCore;

namespace API
{
    public static class Startup
    {
        public static void IgnoreCycles(IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        }

        public static void AddCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            });
        }

        public static void SetImplementations(IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnityService, UnityService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAlternativeService, AlternativeService>();
            services.AddScoped<IAnswerService, AnswerService>();

            services.AddScoped<QuestionnaireUseCase>();
            services.AddScoped<UsersUseCase>();
            services.AddScoped<UnityUseCase>();
            services.AddScoped<LessonUseCase>();
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
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

        public static void ConfigureCors(WebApplication app)
        {
            app.UseCors("AllowFrontend");
        }

        public static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}