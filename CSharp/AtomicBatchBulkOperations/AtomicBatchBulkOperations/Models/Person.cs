using AtomicBatchBulkOperations.Enumerations;
using System;

namespace AtomicBatchBulkOperations.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public string ExtendedInfo { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
