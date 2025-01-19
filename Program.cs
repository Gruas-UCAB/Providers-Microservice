using DotNetEnv;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProvidersMicroservice.core.Application;
using ProvidersMicroservice.core.Infrastructure;
using ProvidersMicroservice.src.crane.application.commands.create_crane.types;
using ProvidersMicroservice.src.provider.application.repositories;
using ProvidersMicroservice.src.provider.infrastructure.repositories;
using ProvidersMicroservice.src.provider.infrastructure.validators;
using ProvidersMicroservice.src.providers.application.commands.create_provider.types;
using RestSharp;
using System.Text;
using UsersMicroservice.core.Application;
using UsersMicroservice.core.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<MongoDBConfig>();
builder.Services.AddSingleton<IRestClient>(sp => new RestClient());
builder.Services.AddTransient<IValidator<CreateCraneCommand>, CreateCraneCommandValidator>();
builder.Services.AddScoped<ICraneRepository, MongoCraneRepository>();
builder.Services.AddTransient<IValidator<CreateProviderCommand>, CreateProviderCommandValidator>();
builder.Services.AddScoped<IProviderRepository, MongoProviderRepository>();
builder.Services.AddScoped<IIdGenerator<string>, UUIDGenerator>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY")!)),
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CreationalUser", policy => policy.RequireClaim("UserRole", ["admin", "provider"]));
    options.AddPolicy("AdminUser", policy => policy.RequireClaim("UserRole", "admin"));
});
builder.Services.AddScoped<ITokenAuthenticationService, JwtService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Providers Microservice API", Version = "v1" });
}); ;


var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Providers Microservice API v1");

});

app.Run();
