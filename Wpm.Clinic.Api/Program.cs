using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Api.Application;
using Wpm.Clinic.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ClinicApplicationService>();
builder.Services.AddDbContext<ClinicDbContext>(options =>
{
    options.UseSqlite("Data Source=WpmClinic.db");
});
builder.Services.AddControllers();

var app = builder.Build();
app.EnsureDbIsCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();