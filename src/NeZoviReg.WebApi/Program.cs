using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using NeZoviReg.Auth.Authentication;
using NeZoviReg.Auth.Authorization;
using NeZoviReg.Auth.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o =>
{
    o.ConfigureHttpsDefaults(m => m.ClientCertificateMode = ClientCertificateMode.RequireCertificate);
});

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Services.AddNeZoviRegAuthentication();
/*builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<NeZoviRegCertAuthenticationOptionsSetup>();*/
builder.Services.AddSingleton<IAuthorizationHandler, PermissionRequirementHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddTransient<INeZoviRegAuthorizationService, NeZoviRegAuthorizationService>();
builder.Services.AddTransient<ICertValidationService, CertValidationService>();

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
