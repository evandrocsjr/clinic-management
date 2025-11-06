using DDDProject.Domain.ValueObjects;
using Wpm.SharedKernel;
using Xunit;

namespace DDDProject.Domain.Tests;

public class PetTest
{
    [Fact]
    public void Create_ShouldBeEqual()
    {
        var id = Guid.NewGuid();
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;
        var pet1 = new Pet(
                id,
                "Test pet 1",
                44,
                "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        var pet2 = new Pet(
                id,
                "Test pet 1",
                44,
                "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        
        Assert.Equal(pet1, pet2);
    }

    [Fact]
    public void Equals_ShouldBeEqual()
    {
        var id = Guid.NewGuid();
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;
        var pet1 = new Pet(
                id,
                "Test pet 1",
                44, 
                "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        var pet2 = new Pet(
                id, 
                "Test pet 1",
                44,
                "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );

        Assert.True(pet1 == pet2);
    }
    
    [Fact]
    public void Equals_ShouldNotEqual()
    {
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;
        var pet1 = new Pet(
            Guid.NewGuid(),
            "Test pet 1",
            44, 
            "red",
            SexOfPet.MALE,
            new BreedId(breedId, breedService)
        );
        var pet2 = new Pet(
            Guid.NewGuid(), 
            "Test pet 1",
            44,
            "red",
            SexOfPet.MALE,
            new BreedId(breedId, breedService)
        );

        Assert.True(pet1 != pet2);
    }

    [Fact]
    public void Weight_ShouldBeEqual()
    {
        var weight = new Weight(33.33m);
        var weight2 = new Weight(33.33m);
        
        // Asserts
        
        Assert.Equal(weight, weight2);
    }

    [Fact]
    public void WeightRange_ShouldBeEqual()
    {
        var range1 = new WeightRange(22.4m, 44.2m);
        var range2 = new WeightRange(22.4m, 44.2m);
        
        Assert.Equal(range1, range2);
        Assert.True(range1 == range2);
    }

    [Fact]
    public void BreedId_ShouldBeValid()
    {
        // Arrange
        var service = new FakeBreedService();
        var breedId = service.Breeds[0].Id;
        
        // Act
        var breed = new BreedId(breedId, service);
        
        // Asserts
        Assert.NotNull(breed);
    }

    [Fact]
    public void BreedId_ShouldNotBeValid()
    {
        // Arrange
        var service = new FakeBreedService();
        
        // Act + Asserts
        Assert.Throws<ArgumentException>(() =>
        {
            var breeId = new BreedId(Guid.NewGuid(), service);
        });
    }

    [Fact]
    public void WeightClass_ShouldBeIdeal()
    {
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;
        var pet = new Pet(
                Guid.NewGuid(), 
                "Test pet 1",
                20,
                "blue",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        pet.SetWeight(new Weight(20), breedService);
        
        Assert.Equal(WeightClass.Ideal, pet.WeightClass);
    }
    
    [Fact]
    public void WeightClass_ShouldBeUnderweight()
    {
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;

        var pet = new Pet(
                Guid.NewGuid(), 
                "Test pet 2",
                33,
            "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        
        
        pet.SetWeight(80, breedService);
        
        Assert.Equal(WeightClass.Underweight, pet.WeightClass);
    }
    
    [Fact]
    public void WeightClass_ShouldBeOverweight()
    {
        var breedService = new FakeBreedService();
        var breedId = breedService.Breeds[0].Id;

        var pet = new Pet(
                Guid.NewGuid(), 
                "Test pet 2",
                33,
            "red",
                SexOfPet.MALE,
                new BreedId(breedId, breedService)
            );
        
        
        pet.SetWeight(11, breedService);
        
        Assert.Equal(WeightClass.Overweight, pet.WeightClass);
    }
}