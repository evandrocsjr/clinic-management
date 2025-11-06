using DDDProject.Domain;
using DDDProject.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Wpm.Management.Api.Infrastructure;

public class ManagementDbContext : DbContext
{
    public ManagementDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Pet>().HasKey(x => x.Id);
        
        modelBuilder.Entity<Pet>().Property(x => x.BreedId)
            .HasConversion(x => x.Value, x => BreedId.Create(x));
        
        modelBuilder.Entity<Pet>().OwnsOne(x => x.Weight);
    }

    public DbSet<Pet> Pets { get; set; }
}

public static class ManagementDbContextExtensions
{
    public static void EnsureDbIsCreated(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetService<ManagementDbContext>();
        context.Database.EnsureCreated();
        context.Database.CloseConnection();
    }
}

public class ManagementRepository(ManagementDbContext dbContext) : IManagementRepository
{
    public Pet? GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Pet> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Insert(Pet pet)
    {
        throw new NotImplementedException();
    }

    public void Update(Pet pet)
    {
        throw new NotImplementedException();
    }

    public void Delete(Pet pet)
    {
        throw new NotImplementedException();
    }
}