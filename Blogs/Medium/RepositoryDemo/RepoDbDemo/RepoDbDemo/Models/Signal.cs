using System;

namespace RepoDbDemo.Models
{
    public class Signal
    {
        public int Id { get; set; }
        public int SignalTypeId { get; set; }
        public int SignalSourceId { get; set; }
        public decimal Value { get; set; }
    }
}
