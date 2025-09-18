using API.Extension;
using core.application;
using FluentValidation;
using FluentValidation.AspNetCore;
using infrastructure.persistance;
using infrastructure.persistance.contexts;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


const string AnyOriginPolicy = "AnyOrigin";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(AnyOriginPolicy, p =>
        p.AllowAnyOrigin()
         .AllowAnyHeader()
         .AllowAnyMethod());
});

// Add services to the container.
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddPersistanceInfrastructure(builder.Configuration);
builder.Services.AddApplicationLayer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.LoadSwagConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roulette API v1");
    c.RoutePrefix = "swagger";
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", () => Results.Ok(new { pong = true }));

app.Run();
