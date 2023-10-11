using dotenv.net;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Notificator API",
        Description = "<h2>Description</h2>" +
            "<p>An ASP.Net API for sending Telegram messages and emails.</p>" +
            "<h2>Requisites</h2>" +
            "<p>For using Telegram services you need to start a chat with the " +
            $"<a href=\"{Environment.GetEnvironmentVariable("TELEGRAM_BOT_URL")}\">Telegram bot</a>.</p>",
    });
});

builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", context =>
    {
        context.Response.Redirect("swagger");
        return Task.CompletedTask;
    });
    endpoints.MapControllers();
});

app.Run();
