using System;

namespace GenericRepository.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDateUtc { get; set; }
    }
}
