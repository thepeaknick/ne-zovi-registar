using NeZoviReg.Composition;
using NeZoviReg.WebApi.Extensions;
using NeZoviReg.WebApi.Extensions.Middleware;
using NeZoviReg.WebApi.Extensions.WebApi;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o =>
{
    /*o.ConfigureHttpsDefaults(m => m.ClientCertificateMode = ClientCertificateMode.AllowCertificate);*/
});

builder.Host.UseSerilog((ctx, lc)
    => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.ConfigureWebApi(builder.Configuration)
                .ConfigureApplication(builder.Configuration);

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

app.UseRateLimiter();

app.UseMiddleware<ExceptionsHandlingMiddleware>();

app.Run();
