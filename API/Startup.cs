using System.Text;
using API.Middlewares;
using Application.UseCases;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Infra;
using Infra.Repositories;
using Infra.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace API
{
    internal static class Startup
    {
        internal static void IgnoreCycles(IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        }

        internal static void AddCors(IServiceCollection services)
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

        internal static void SetImplementations(IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            // repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IUnityRepository, UnityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // services
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAlternativeService, AlternativeService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<ICertificateService, CertificateService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IJwtService, JwtService>();

            // usecases
            services.AddScoped<GetLessonsUseCase>();
            services.AddScoped<GetLessonUseCase>();
            services.AddScoped<IssueCertificateUseCase>();
            services.AddScoped<LoginUseCase>();
            services.AddScoped<RegisterUseCase>();
            services.AddScoped<VerifyAnswersUseCase>();
            services.AddScoped<GetUnityUseCase>();
        }

        internal static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        internal static void ConfigureJwt(IServiceCollection services, ConfigurationManager configuration)
        {
            var key = configuration["JwtConfig:Key"] ?? throw new KeyNotFoundException("The environment was not prepared to set key");

            services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = configuration["JwtConfig:Issuer"],
                    ValidAudience = configuration["JwtConfig:Audience"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

            services.AddAuthorization();
        }

        internal static void ConfigureScalar(WebApplication app)
        {
            app.UseSwagger();
            app.MapScalarApiReference(op =>
            {
                op.AddDocument("Doc", routePattern: "/swagger/v1/swagger.json");
            });
        }

        internal static void ConfigureAPI(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.MapControllers();
        }

        internal static void ConfigureCors(WebApplication app)
        {
            app.UseCors("AllowFrontend");
        }

        internal static void ConfigureMiddlewares(WebApplication app)
        {
            app.UseMiddleware<ApiMiddleware>();
        }

        internal static void UseJwt(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}