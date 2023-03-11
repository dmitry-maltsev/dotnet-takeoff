using DotnetTakeoff.Api.Application.System;
using DotnetTakeoff.Api.Application.Users;
using DotnetTakeoff.Api.Extensions;
using DotnetTakeoff.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();
builder.AddSwagger();
builder.AddCors();
builder.AddAuthentication();
builder.Services.AddHealthChecks();
builder.Services.AddResponseCompression();
builder.Services.AddApplicationInsightsTelemetry();
builder.AddForwardedHeaders();
builder.AddErrorHandling();
builder.AddValidation();

if (!builder.Environment.IsDevelopment())
{
    builder.AddSecrets();
}

builder.AddApplicationServices();

var app = builder.Build();

app.UseForwardedHeaders();
app.UseResponseCompression();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExplorer();
}

app.UseHealthChecks("/health");
app.UseSerilog();
app.UseErrorHandling();
app.UseCors();
app.UseAuthentication();

app.MapSystemRoutes();
app.MapUsersRoutes();

app.Run();
