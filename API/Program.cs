using API;

var builder = WebApplication.CreateBuilder(args);

Startup.SetImplementations(builder.Services);
Startup.ConfigureSwagger(builder.Services);

var app = builder.Build();

Startup.ConfigureScalar(app);
Startup.ConfigureAPI(app);

await app.RunAsync();