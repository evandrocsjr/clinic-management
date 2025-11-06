using DDDProject.Domain;
using Microsoft.EntityFrameworkCore;
using Wpm.Mangament.Api.Application;
using Wpm.Mangament.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ManagementDbContext>(options =>
{
    options.UseSqlite("Data Source=WpmManagement.db");
});
builder.Services.AddScoped<IManagementRepository, ManagementRepository>();
builder.Services.AddScoped<IBreedService, BreedService>();
builder.Services.AddScoped<ICommandHandler<SetWeightCommand>, SetWeightCommandHandler>();
builder.Services.AddScoped<ManagementAplicationService>();
var app = builder.Build();
app.EnsureDbIsCreated();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();

