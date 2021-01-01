using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkRawSQL.Models
{
    [Table("Person")]
    public class AnotherPerson
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedDateUtc { get; set; }
    }
}
