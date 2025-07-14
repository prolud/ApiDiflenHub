using API;

var builder = WebApplication.CreateBuilder(args);

Startup.SetImplementations(builder.Services);
Startup.ConfigureSwagger(builder.Services);
Startup.AddCors(builder.Services);
Startup.IgnoreCycles(builder.Services);
// Startup.ConfigureJwt(builder.Services, builder.Configuration);

var app = builder.Build();

Startup.ConfigureScalar(app);
Startup.ConfigureAPI(app);
Startup.ConfigureCors(app);
Startup.ConfigureMiddlewares(app);
// Startup.UseJwt(app);

await app.RunAsync();