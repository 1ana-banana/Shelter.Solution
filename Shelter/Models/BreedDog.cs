using System.ComponentModel.DataAnnotations;

namespace Shelter.Models
{
  public class BreedDog
  {
    [Key]
    public int BreedDogId {get; set;}
    public int DogId {get; set;}
    public int BreedId {get; set;}
    public virtual Dog Dog {get; set;}
    public virtual Breed Breed {get; set;}
  }
}