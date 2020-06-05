using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Order
  {
    public Order()
    {
      this.Orders = new HashSet<FlavorTreat>();
    }

    public int OrderId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public int Quantity { get; set; }
    public ICollection<CategoryItem> Categories { get; }
  }
}