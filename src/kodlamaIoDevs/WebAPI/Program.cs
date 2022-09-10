using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Encryption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistance;

var builder = WebApplication.CreateBuilder(args);
var tokenOptions = builder.Configuration.GetSection(key: "TokenOptions").Get<TokenOptions>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();
builder.Services.AddPersistenceServices(builder.Configuration);
// builder.Services.AddInfrastructureServices();
// builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime=true,
        ValidIssuer=tokenOptions.Issuer,
        ValidAudience=tokenOptions.Audience,
        IssuerSigningKey=SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthorization();

app.MapControllers();

app.Run();

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();
