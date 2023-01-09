using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using KlondikeWebApi.Data;
using KlondikeWebApi.Entities;
using KlondikeWebApi.Repositories;
using KlondikeWebApi.Token;

var builder = WebApplication.CreateBuilder(args);

var clientUrl = builder.Configuration["ClientUrl"] ??
    Environment.GetEnvironmentVariable("CLIENT_URL") ??
    throw new InvalidOperationException("CLIENT_URL not found.");

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

var jwtIssuer = builder.Configuration["JwtIssuer"] ??
    Environment.GetEnvironmentVariable("JWT_ISSUER") ??
    throw new InvalidOperationException("JwtIssuer not found.");

var jwtSecretKey = builder.Configuration["JwtSecretKey"] ??
    Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ??
    throw new InvalidOperationException("JwtSecretKey not found.");

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddIdentity<UserEntity, RoleEntity>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = clientUrl,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey))
        };
    });

builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddTransient<ITokenService, TokenService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins(clientUrl)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(myAllowSpecificOrigins);

app.Run();

public partial class Program {}
