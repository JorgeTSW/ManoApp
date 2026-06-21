using ManoApp.Api.Auth;
using ManoApp.Application.Services;
using ManoApp.Domain.Interfaces;
using ManoApp.Domain.Services;
using ManoApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IGestureClassifier, GestureClassifier>();

var logsFolder = Path.Combine(builder.Environment.ContentRootPath, "data", "logs");
builder.Services.AddSingleton<IGestureLogRepository>(new CsvGestureLogRepository(logsFolder));

builder.Services.AddScoped<GestureLoggingService>();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowManoAppWeb", policy =>
    {
        policy.WithOrigins("http://localhost:5031") // puerto de ManoApp.Web
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Nuevo
builder.Services.Configure<List<AppUser>>(builder.Configuration.GetSection("Users"));

var jwtSecretKey = builder.Configuration["Jwt:SecretKey"]!;
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowManoAppWeb");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();