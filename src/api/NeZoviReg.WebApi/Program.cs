using NeZoviReg.Composition;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(o =>
{
    /*o.ConfigureHttpsDefaults(m => m.ClientCertificateMode = ClientCertificateMode.AllowCertificate);*/
});

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

builder.Host.UseSerilog((ctx, lc)
    => lc.ReadFrom.Configuration(ctx.Configuration));

builder.Services.ConfigureNeZoviRegApp(builder.Configuration);

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
