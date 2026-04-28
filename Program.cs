var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET /games 
app.MapGet("/games", () => "Hello World!");

app.Run();

WebApplication.CreateBuilder(args).Build().MapGet("/", () => "Hello World!");
