using Identity;
using Identity.BackGrounds;
using Identity.Configurations;
using Identity.Handlers;
using Identity.Models.Contexts;
using Identity.Models.Entities;
using Identity.Services.Auth;
using Identity.Services.Role;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.Smo.Wmi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContext<AppDbContext>(_ => _.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddIdentity<AppUser, AppRole>(_ =>
{
    _.Password.RequiredLength = 5;
    _.Password.RequireNonAlphanumeric = false;
    _.Password.RequireLowercase = true;
    _.Password.RequireUppercase = true;
    _.Password.RequireDigit = false;
    _.User.RequireUniqueEmail = true;
}

).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
    };
});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITokenHandler, Identity.Handlers.TokenHandler>();



builder.Services.AddControllers();



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


