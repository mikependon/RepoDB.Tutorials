using System.Collections.Generic;

namespace CleanAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
