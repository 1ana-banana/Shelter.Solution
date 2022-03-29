namespace Shelter.Models
{
  public class PriorityDog
  {
    public int PriorityDogId {get; set;}
    public int PriorityId {get; set;}
    public int DogId {get ;set;}
    public virtual Priority Priority {get; set;}
    public virtual Dog Dog {get; set;}
  }
}