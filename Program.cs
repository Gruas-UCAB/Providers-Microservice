using DotNetEnv;
using FluentValidation;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.infrastructure.repositories;
using ProvidersMicroservice.src.provider.infrastructure.validators;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;
using UsersMicroservice.core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddTransient<IValidator<CreateCraneCommand>, CreateCraneCommandValidator>();
builder.Services.AddScoped<ICraneRepository, MongoCraneRepository>();
builder.Services.AddTransient<IValidator<CreateProviderCommand>, CreateProviderCommandValidator>();
builder.Services.AddScoped<IProviderRepository, MongoProviderRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
