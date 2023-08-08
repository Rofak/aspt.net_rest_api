using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Restfull.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Restfull.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("Default"), new MySqlServerVersion(new Version(8, 0, 29)))
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    var bearerSchema = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    };
    var apiKeySchema = new OpenApiSecurityScheme 
    {
        Name = "x-api-key",
        Description = "Enter `X-API-KEY`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "x-api-key"
    };
    option.AddSecurityDefinition(name: "Bearer", securityScheme: bearerSchema);
    option.AddSecurityDefinition(name: "X-API-KEY", securityScheme: apiKeySchema);
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Reference = new OpenApiReference
                    {
                        Id = "X-API-KEY",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
            }
        });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");  // TODO: Store Authuntication without losing when refresh page
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ApiKeyMiddleware>();
app.MapControllers();

app.Run();
