using Serilog;
using Cepedi.Serasa.Cadastro.IoC;
using Cepedi.Serasa.Cadastro.Api.Extension;
using Cepedi.Serasa.Cadastro.Domain.Configuration;
using Cepedi.Serasa.Cadastro.Domain.Services;
using Cepedi.Serasa.Cadastro.Domain.Services.Implementation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CEPEDI.SERASA.CADASTRO API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Authorization header using the Bearer scheme."
    });;
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
});

// Bind JWT settings from configuration
var jwtConfig = builder.Configuration.GetSection("Jwt").Get<TokenConfiguration>();
builder.Services.Configure<TokenConfiguration>(builder.Configuration.GetSection("Jwt"));

// Register token service
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.ConfigureAppDependencies(builder.Configuration);

// Configure JWT authentication
var key = Encoding.UTF8.GetBytes(jwtConfig.Secret);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidAudience = jwtConfig.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// Configure Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Map("/", () => Results.Redirect("/swagger"));

app.Run();
