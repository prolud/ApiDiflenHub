using API;

var builder = WebApplication.CreateBuilder(args);

Startup.SetImplementations(builder.Services);
Startup.ConfigureSwagger(builder.Services);
Startup.AddCors(builder.Services);
Startup.IgnoreCycles(builder.Services);

var app = builder.Build();

Startup.ConfigureScalar(app);
Startup.ConfigureAPI(app);
Startup.ConfigureCors(app);
Startup.ConfigureMiddlewares(app);

await app.RunAsync();