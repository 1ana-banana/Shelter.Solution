using System.Collections.Generic;

namespace Shelter.Models
{
  public class Priority
  {
    public int PriorityId {get; set;}
    public int Stars {get; set;}

    public Priority()
    {
      this.JoinRR = new HashSet<PriorityDog>();
    }
    public virtual ICollection<PriorityDog> JoinRR { get; }
    public virtual ApplicationUser User { get; set; }
  }
}