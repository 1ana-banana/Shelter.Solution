using System.Collections.Generic;

namespace Shelter.Models
{
  public class Breed
  {
    public Breed()
    {
      this.JoinIR = new HashSet<BreedDog>();
    }
    public int BreedId {get; set;}
    public string Name {get; set;}
    public virtual ApplicationUser User {get; set;}
    public virtual ICollection<BreedDog> JoinIR {get; set;}
  }
}