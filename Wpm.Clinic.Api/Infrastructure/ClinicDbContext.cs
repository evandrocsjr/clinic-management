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

    public DbSet<Consultation> Consultations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Consultation>(consultation =>
        {
            consultation.HasKey(c => c.Id);

            consultation.Property(c => c.PatientId)
                .HasConversion(c => c.Value, (c) => new PatientId(c));

            consultation.OwnsOne(c => c.Diagnosis);
            consultation.OwnsOne(c => c.Treatment);
            consultation.OwnsOne(c => c.CurrentWeight);
            consultation.OwnsOne(c => c.When);
        });

        modelBuilder.Entity<DrugAdministration>(drugAdministrations =>
        {
            drugAdministrations.HasKey(d => d.Id);

            drugAdministrations.OwnsOne(d => d.Dose);

            drugAdministrations.OwnsOne(d => d.DrugId);
        });
    }
    
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
