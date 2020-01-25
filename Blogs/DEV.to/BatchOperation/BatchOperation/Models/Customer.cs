using System;

namespace BatchOperation.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public string Address { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
    }
}
