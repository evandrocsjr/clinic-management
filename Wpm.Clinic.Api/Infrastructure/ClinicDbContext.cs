using Microsoft.EntityFrameworkCore;
using Wpm.Clinic.Domain;
using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValuesObjects;

namespace Wpm.Clinic.Api.Infrastructure;

public class ClinicDbContext : DbContext
{
    public ClinicDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ConsultationEventData> Consultations { get; set; }
    
}
public static class ManagementDbContextExtensions
{
    public static void EnsureDbIsCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ClinicDbContext>();
        context.Database.EnsureCreated();
        context.Database.CloseConnection();
    }
}

public record ConsultationEventData(
    Guid Id,
    string AggregateId,
    string eventName,
    string Data,
    string AssemblyQualifiedName
    );
