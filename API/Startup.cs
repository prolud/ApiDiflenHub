using System.Text;
using API.Middlewares;
using Application.Auth;
using Application.UseCases;
using Domain.Interfaces;
using Infra;
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUnityService, UnityService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IAlternativeService, AlternativeService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<QuestionnaireUseCase>();
            services.AddScoped<UserUseCase>();
            services.AddScoped<UnityUseCase>();
            services.AddScoped<LessonUseCase>();

            services.AddScoped<JwtService>();
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
            app.UseAuthorization();
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