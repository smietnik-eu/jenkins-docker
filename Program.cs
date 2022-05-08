var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "\nHello World!\n\n");

app.Run();
