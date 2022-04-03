using System;
using System.Collections.Generic;

namespace Shelter.Models
{
  public class Dog
  {
    public Dog()
    {
      this.JoinIR = new HashSet<BreedDog>();
      this.JoinRR = new HashSet<PriorityDog>();
    }
    public int DogId {get; set;}
    public string Title {get ;set;}
    public string Description {get; set;}
    public DateTime Birthday {get; set;}
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<BreedDog> JoinIR { get; }
    public virtual ICollection<PriorityDog> JoinRR { get; }

  }
}