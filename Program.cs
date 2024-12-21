using DotNetEnv;
using FluentValidation;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.crane.application.repositories;
using ProvidersMicroservice.src.crane.infrastructure.repositories;
using ProvidersMicroservice.src.crane.infrastructure.validators;
using UsersMicroservice.core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddTransient<IValidator<CreateCraneCommand>, CreateCraneCommandValidator>();
builder.Services.AddScoped<ICraneRepository, MongoCraneRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
