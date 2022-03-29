using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Shelter.Models
{
  public class ShelterContext : IdentityDbContext<ApplicationUser>
  {
    public virtual DbSet<Breed> Breeds { get; set; }
    public DbSet<Dog> Dogs { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<BreedDog> BreedDogs { get; set; }
    public DbSet<PriorityDog> PriorityDogs { get; set; }

    public ShelterContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}