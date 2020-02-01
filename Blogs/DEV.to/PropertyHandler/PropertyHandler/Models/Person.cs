using PropertyHandler.PropertyHandlers;
using RepoDb.Attributes;
using System;

namespace PropertyHandler.Models
{
    public class Person
    {
        public long Id { get; set; }

        public string Name { get; set; }

        [PropertyHandler(typeof(PersonAgeHandler))]
        public string Age { get; set; }

        [PropertyHandler(typeof(PersonExtendedInfoHandler))]
        public PersonExtendedInfo ExtendedInfo { get; set; }

        public DateTime CreatedDateUtc { get; set; }
    }
}
